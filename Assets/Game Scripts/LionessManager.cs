using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class LionessManager : PatrolAnimatronic {

    private int trackingDistance;
    private int randomAngle;
    private float targetMoveX;
    private float targetMoveZ;

    public float STALK_SPEED = 1.5f;
    public float CHASE_SPEED = 7f;

    private GameObject TrackingPoint;
    private Vector3 destination;
    private LineRenderer LineRenderer;
    Color DebugColor = new Color(255, 224, 32, 255);

    // Start is called before the first frame update
    public override void Start() {
        base.Start();
        //level = 0;
        CanSeePlayer = false;
        actionTimer = 21 - Level;
        NavMeshAgent.speed = STALK_SPEED;
        InitLineRenderer();
        TrackingPoint = GameObject.Find("Lioness Target Point");
        pursuitMode = PursuitMode.Patrol;
    }

    private void FixedUpdate() {
    }

    // Update is called once per frame
    void Update() {
        if (Level == 0) return;
        //CharacterController.Move(NavMeshAgent.destination * Time.deltaTime * movementSpeed);
        //NavMeshAgent.Move(NavMeshAgent.destination * Time.deltaTime * movementSpeed);

        LineRenderer.SetPosition(0, transform.position);

        switch (pursuitMode) {
        case PursuitMode.Stop: // Do Nothing if I don't want the animatronic to, basically just AI 0
            return;

        case PursuitMode.Patrol:
            if (CanSeePlayer) { // Check to see if the player is in detection range, if they are, start a chase
                pursuitMode = PursuitMode.Chase;
                NavMeshAgent.speed = CHASE_SPEED;
                return;
            }
            timeSinceAction += Time.deltaTime; // Check time since the previous action, if a long enough time, change the destination
            if (timeSinceAction < actionTimer) {
                return;
            }
            randomRoll = Random.Range(0, 20); // Set destination to a random location within a random location 'level' units away from the player (does ignore walls)
            trackingDistance = getTrackingDistance(randomRoll);
            randomAngle = Random.Range(0, 359);
            targetMoveX = Player.GetComponent<Transform>().position.x + (trackingDistance * Mathf.Sin(randomAngle));
            targetMoveZ = Player.GetComponent<Transform>().position.z + (trackingDistance * Mathf.Cos(randomAngle));

            destination = new Vector3(targetMoveX, Player.GetComponent<Transform>().position.y, targetMoveZ);
            timeSinceAction = 0f;
            break;

        case PursuitMode.Chase:
            EndChaseCheck();
            destination = Player.GetComponent<Transform>().position; // Just fucking book it towards the player
            break;

        case PursuitMode.JustExitedChase:
            destination = transform.position; //Stay where you are
            PauseTimer += Time.deltaTime; // Timer to reset back to patrol mode
            if (PauseTimer >= PauseLimit) {
                pursuitMode = PursuitMode.Patrol;
                NavMeshAgent.speed = STALK_SPEED;
                PauseTimer = 0;
                timeSinceAction = actionTimer; // Ensures movement immediatly after unpausing
            }
            break;
        }
        NavMeshAgent.SetDestination(destination);
        moveTrackingPoint();
    }

    int getTrackingDistance(int randomRole) => randomRole - Level >= 0 ? randomRole - Level : 0;
    void moveTrackingPoint() {
        TrackingPoint.GetComponent<Transform>().position = new Vector3(destination.x, 30f, destination.z);
        LineRenderer.SetPosition(1, TrackingPoint.GetComponent<Transform>().position);
    }

    public override void EndChaseCheck() {
        if (!CanSeePlayer) {
            ChaseTimer += Time.deltaTime;
            if (ChaseTimer >= ChaseLimit) {
                pursuitMode = PursuitMode.JustExitedChase;
                ChaseTimer = 0;
            }
        }
        else
            ChaseTimer = 0;
    }

    void InitLineRenderer() {
        LineRenderer = new GameObject("Lioness Travel Line").AddComponent<LineRenderer>();
        LineRenderer.startColor = DebugColor;
        LineRenderer.endColor = DebugColor;
        LineRenderer.startWidth = 2f;
        LineRenderer.endWidth = 2f;
        LineRenderer.positionCount = 2;
        LineRenderer.useWorldSpace = true;
    }
}

    
