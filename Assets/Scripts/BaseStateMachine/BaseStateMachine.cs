using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseStateMachine: MonoBehaviour
{
    protected BaseState currentState;

    protected virtual void Update()
    {
        if(currentState != null)
        {
            currentState.UpdateState();
        }
    }

    //called at Start() of stateMachine
    public void InitializedStateMachine(BaseState startingState)
    {
        currentState = startingState;
        currentState.EnterState();
    }

    public void ChangeState(BaseState nextState)
    {
        if(currentState != null)
        {
            currentState.ExitState();
        }

        currentState = nextState;
        currentState.EnterState();
    }

}
