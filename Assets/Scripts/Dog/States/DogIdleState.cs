using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogIdleState : BaseState
{
    [SerializeField] float waitTimeAtPath;

    //injected values
    private MovementData movementData;
    private StaminaComponent staminaComponent;

    public DogIdleState(BaseStateMachine stateMachine, Animator animator, DogMovementData dogMovementData)
        : base(stateMachine, animator)
    {
        movementData = dogMovementData.movementData;
        staminaComponent = dogMovementData.staminaComponent;
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
        if (staminaComponent)
        {
            //stop recharging 
            staminaComponent.StopRechargeStamina();
        }

        // allow movement
        movementData.agent.isStopped = false;
    }

    protected override void OnUpdateState()
    {
        
    }

    private IEnumerator WaitForPatrol()
    {
        if(staminaComponent)
        {
            //recharge
            staminaComponent.HandleRechargeStamina();
        }

        yield return new WaitForSeconds(waitTimeAtPath);

        DogStateMachine dogStateMachine = stateMachine as DogStateMachine;
        stateMachine.ChangeState(dogStateMachine.PatrolState);
    }
}
