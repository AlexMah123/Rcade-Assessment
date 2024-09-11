using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementData
{
    public MovementData movementData;
    public float waitTimeAtPath;
    public float fleeDistance;

    public NPCMovementData(MovementData movementData, float waitTimeAtPath, float fleeDistance)
    {
        this.movementData = movementData;
        this.waitTimeAtPath = waitTimeAtPath;
        this.fleeDistance = fleeDistance;
    }
}

[RequireComponent(typeof(MovementComponent))]
public class NPCStateMachine : BaseStateMachine
{
    [Header("NPC Configs")]
    [SerializeField] float waitTimeAtPath = 4f;
    [SerializeField] float fleeDistance = 4f;

    public NPCIdleState IdleState;
    public NPCPatrolState PatrolState;
    public NPCFleeState FleeState;

    private Animator animator;
    private MovementComponent movementComponent;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movementComponent = GetComponent<MovementComponent>();
    }

    private void Start()
    {
        InitializeData();
        InitializeStateMachine(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FleeTarget"))
        {
            ChangeState(FleeState);
        }

        if (currentState != null)
        {
            currentState.OnTriggerEnter(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (currentState != null)
        {
            currentState.OnTriggerStay(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("FleeTarget"))
        {
            ChangeState(IdleState);
        }

        if (currentState != null)
        {
            currentState.OnTriggerExit(other);
        }
    }

    private void InitializeData()
    {
        if (movementComponent == null)
        {
            Debug.LogError("MovementComponent is not attached");
        }

        NPCMovementData npcMovementData = new(movementComponent.movementData, waitTimeAtPath, fleeDistance);

        IdleState = new(this, animator, npcMovementData);
        PatrolState = new(this, animator, npcMovementData);
        FleeState = new(this, animator, npcMovementData);
    }
}
