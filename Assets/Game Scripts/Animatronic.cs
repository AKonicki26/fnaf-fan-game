using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

abstract public class Animatronic : MonoBehaviour, ISetAiLevel
{
    public Vector3 StartingPosition = new Vector3(0f, 2f, 0f);

    
    private int _level = 0;
    [SerializeField]
    public int Level {
        get => _level;
        set {
            _level = value;
        }
    }
    
    public float actionTimer;
    public float timeSinceAction = 0f;
    public int randomRoll;
    public NavMeshAgent NavMeshAgent;
    [SerializeField]
    protected bool KillEnabled;

    [SerializeField]
    public GameObject Player;
    protected float JumpscareRadius;
    [SerializeField]
    public LayerMask PlayerLayerMask;

    // Start is called before the first frame update
    public virtual void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        //Set animatronics to starting position then make sure they're not in the ground by using CC.Move()
        //transform.position = StartingPosition; 
        //CharacterController.Move(StartingPosition);
        JumpscareRadius = GetComponent<CapsuleCollider>().radius + (Player.GetComponentInChildren<CapsuleCollider>().radius * 2);
        StartCoroutine(CheckForPlayerJumpscare());
        KillEnabled = false;
    }

    public void LateUpdate() {
        //Debug.Log($"{gameObject.name} Level: {level}");
    }

    public virtual void JumpscarePlayer() {
        if (Player_Manager.IsAlive) {
            Player.transform.LookAt(new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z));
            Player_Manager.IsAlive = false;
        }
        NavMeshAgent.speed = 0f;
    }

    private IEnumerator CheckForPlayerJumpscare() {
        while (true) {
            yield return new WaitForSeconds(0.2f);
            if (Physics.OverlapSphere(transform.position, JumpscareRadius, PlayerLayerMask).Length != 0 && KillEnabled)
                JumpscarePlayer();
        }
    }

    public virtual void SetAiLevel(int level) {
        this.Level = level;
    }
}
