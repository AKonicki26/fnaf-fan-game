using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Criket_AI : PatrolAnimatronic
{
    // Start is called before the first frame update

    [SerializeField]
    private float PatrolSpeed;
    [SerializeField]
    private float ChaseSpeed;

    private Vector3 NextPatrolPosition;
    private List<Vector3> PatrolPositions = new();

    private enum Rooms {
        MainArea,
        MainStage,
        BackStage,
        Arcade,
        Kitchen
    }
    [SerializeField]
    private Rooms CurrentRoom;
    [SerializeField]
    private Rooms TargetRoom = Rooms.MainArea;

    [SerializeField]
    private bool FlashLightInView = false;


    public override void Start()
    {
        StartingPosition = transform.position;
        base.Start();
        TargetRoom = Rooms.MainArea;
        if (Level == 0)
            return;
        StartCoroutine(RNGCouroutine());
        StartCoroutine(FlashLightDetection());
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, NextPatrolPosition) <= 0.1f)
            NextPatrolPosition = (PatrolPositions[(PatrolPositions.IndexOf(NextPatrolPosition) + 1) % PatrolPositions.Count]);  // Set the next patrol position, looping through
    }

    private IEnumerator RNGCouroutine() {
        while(true) {
            randomRoll = UnityEngine.Random.Range(0, 20);
            if (randomRoll >= Level)
                yield return new WaitForSeconds(4.5f);
            Array values = Enum.GetValues(typeof(Rooms));
            TargetRoom = (Rooms)values.GetValue(UnityEngine.Random.Range(0, values.Length));
        }
    }

    private IEnumerator FlashLightDetection() {
        while (true) {
            if (transform.position == StartingPosition)
                yield return new WaitForSeconds(1);
            Vector3 FlashLightEndPosition = Player.GetComponent<Player_Manager>().FlashlightEndPosition;
            Vector3 directionToFlashlight = (FlashLightEndPosition - transform.position).normalized;
            float distanceToFlashlight = Vector3.Distance(FlashLightEndPosition, transform.position);
            FlashLightInView = (Vector3.Angle(transform.forward, directionToFlashlight) < DetectionAngleDegrees / 2) ?
                !Physics.Raycast(transform.position, directionToFlashlight, distanceToFlashlight, MapLayerMask)
                : false;
            yield return new WaitForSeconds(0.2f);
        }
    }

}
