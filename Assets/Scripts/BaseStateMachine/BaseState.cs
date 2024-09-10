using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState
{
    protected BaseStateMachine stateMachine;
    protected Animator animator;

    public BaseState(BaseStateMachine stateMachine, Animator animator)
    {
        this.stateMachine = stateMachine;
        this.animator = animator;
    }

    protected BaseState(BaseStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public void EnterState()
    {
        OnEnterState();
    }

    public void UpdateState()
    {
        OnUpdateState();
    }

    public void ExitState()
    {
        OnExitState();
    }

    protected abstract void OnEnterState();
    protected abstract void OnUpdateState();
    protected abstract void OnExitState();
}
