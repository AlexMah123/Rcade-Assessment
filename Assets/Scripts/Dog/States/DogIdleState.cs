using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogIdleState : BaseState
{
    [SerializeField] float waitTimeAtPath;

    //injected values
    private MovementData movementData;

    public DogIdleState(BaseStateMachine stateMachine, Animator animator, DogMovementData dogMovementData)
        : base(stateMachine, animator)
    {
        movementData = dogMovementData.movementData;

        waitTimeAtPath = dogMovementData.waitTimeAtPath;
    }

    protected override void OnEnterState()
    {
        //set animation
        animator.SetInteger("State", 0);

        stateMachine.StartCoroutine(WaitForPatrol());
    }

    protected override void OnExitState()
    {
        // allow movement
        movementData.agent.isStopped = false;
    }

    protected override void OnUpdateState()
    {
        
    }

    private IEnumerator WaitForPatrol()
    {
        yield return new WaitForSeconds(waitTimeAtPath);

        DogStateMachine dogStateMachine = stateMachine as DogStateMachine;
        stateMachine.ChangeState(dogStateMachine.PatrolState);
    }
}
