using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DogIdleState : BaseState
{
    //injected values
    private MovementData movementData;
    private StaminaComponent staminaComponent;
    float waitTimeAtPath;

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

        // disable movement
        movementData.agent.isStopped = true;
        stateMachine.StartCoroutine(WaitForPatrol());
    }

    protected override void OnUpdateState()
    {

    }

    protected override void OnExitState()
    {
        if (staminaComponent)
        {
            //stop recharging 
            staminaComponent.StopRechargeStamina();
        }
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerStay(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
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
