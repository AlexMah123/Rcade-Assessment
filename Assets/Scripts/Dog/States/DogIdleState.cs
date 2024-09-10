using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogIdleState : BaseState
{
    public DogIdleState(BaseStateMachine stateMachine, Animator animator) : base(stateMachine, animator)
    {
    }

    protected override void OnEnterState()
    {
        animator.SetInteger("State", 1);
    }

    protected override void OnExitState()
    {
        
    }

    protected override void OnUpdateState()
    {
        
    }
}
