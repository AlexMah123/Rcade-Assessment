using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine : BaseStateMachine
{
    public NPCIdleState IdleState;
    public NPCPatrolState PatrolState;
    public NPCFleeState FleeState;

    private Animator animator;


    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        IdleState = new(this, animator);
        PatrolState = new(this, animator);
        FleeState = new(this, animator);
    }

    private void Start()
    {
        InitializedStateMachine(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
