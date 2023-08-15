using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolAnimatronic : Animatronic {
    [Range(0, 30)]
    public float DetectionRadius;
    [Range(0, 5)]
    public float InnerDetectionRadius;
    [Range(0, 360)]
    public float DetectionAngleDegrees;
    public LayerMask MapLayerMask;

    [Range(1,10)]
    public float ChaseLimit;
    [Range(0, 10)]
    public float ChaseTimer;

    [Range(1,10)]
    public float PauseLimit;
    [Range(0,10)]
    public float PauseTimer;

    private bool _canSeePlayer;
    public bool CanSeePlayer {
        get => _canSeePlayer;
        set {
            _canSeePlayer = value;
            KillEnabled = value;
        }
    }
    public enum PursuitMode {
        Patrol,
        Chase,
        Stop,
        JustExitedChase
    };
    public PursuitMode pursuitMode;

    public override void Start() {
        base.Start();
        //Player = GameObject.FindGameObjectWithTag("Player");
        if (Level == 0)
            return;
        StartCoroutine(PlayerDetectionRoutine());
    }
    private IEnumerator PlayerDetectionRoutine() {

        WaitForSeconds pause = new WaitForSeconds(0.2f);

        while (true) {
            yield return pause;
            if (pursuitMode != PursuitMode.JustExitedChase)
                CanSeePlayer = PlayerDetectionCheck();   
        }
    }

    private bool PlayerDetectionCheck() {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, DetectionRadius, PlayerLayerMask);

        if (rangeChecks.Length != 0) {

            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= InnerDetectionRadius)
                return true;

            return (Vector3.Angle(transform.forward, directionToTarget) < DetectionAngleDegrees / 2) ? 
                !Physics.Raycast(transform.position, directionToTarget, distanceToTarget, MapLayerMask) 
                : false;
        }
        return false;
    }

    public override void JumpscarePlayer() {
        base.JumpscarePlayer();
    }

    public virtual void EndChaseCheck() {
        if (!CanSeePlayer && pursuitMode == PursuitMode.Chase) {
            ChaseTimer += Time.deltaTime;
            if (ChaseTimer >= ChaseLimit) {
                pursuitMode = PursuitMode.JustExitedChase;
                ChaseTimer = 0;
            }
        }
        else
            ChaseTimer = 0;
    }   
}
