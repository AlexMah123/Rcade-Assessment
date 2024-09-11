using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))]
public class NPCStateMachine : BaseStateMachine
{
    [Header("NPC Configs")]
    [SerializeField] float waitTimeAtPath = 4f;

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

    private void InitializeData()
    {
        if (movementComponent == null)
        {
            Debug.LogError("MovementComponent is not attached");
        }

        IdleState = new(this, animator);
        PatrolState = new(this, animator);
        FleeState = new(this, animator);
    }
}
