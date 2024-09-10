using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogStateMachine : BaseStateMachine
{
    public DogIdleState IdleState;
    public DogPatrolState PatrolState;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        
        IdleState = new(this, animator);
        PatrolState = new(this, animator);
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
