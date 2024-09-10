using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : BaseState
{
    public NPCIdleState(BaseStateMachine stateMachine, Animator animator) : base(stateMachine, animator)
    {
    }

    protected override void OnEnterState()
    {
        animator.SetInteger("State", 0);
    }

    protected override void OnExitState()
    {
        
    }

    protected override void OnUpdateState()
    {
        
    }
}
