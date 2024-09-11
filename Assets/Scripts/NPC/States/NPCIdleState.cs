using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : BaseState
{
    //injected values
    private MovementData movementData;
    float waitTimeAtPath;

    public NPCIdleState(BaseStateMachine stateMachine, Animator animator, NPCMovementData npcMovementData) : base(stateMachine, animator)
    {
        movementData = npcMovementData.movementData;
        waitTimeAtPath = npcMovementData.waitTimeAtPath;
    }

    protected override void OnEnterState()
    {
        animator.SetInteger("State", 0);

        // disable movement
        movementData.agent.isStopped = true;
        stateMachine.StartCoroutine(WaitForPatrol());
    }

    protected override void OnExitState()
    {

    }

    protected override void OnUpdateState()
    {
        
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
        yield return new WaitForSeconds(waitTimeAtPath);

        NPCStateMachine npcStateMachine = stateMachine as NPCStateMachine;
        stateMachine.ChangeState(npcStateMachine.PatrolState);
    }
}
