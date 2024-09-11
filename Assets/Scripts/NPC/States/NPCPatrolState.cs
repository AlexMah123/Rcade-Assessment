using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCPatrolState : BaseState
{
    //runtime value
    private GameObject currentPathObj;

    //injected values
    private MovementData movementData;

    public NPCPatrolState(BaseStateMachine stateMachine, Animator animator, NPCMovementData npcMovementData) : base(stateMachine, animator)
    {
        movementData = npcMovementData.movementData;
    }

    protected override void OnEnterState()
    {
        //set animation
        animator.SetInteger("State", 1);

        // allow movement
        movementData.agent.isStopped = false;
        movementData.agent.speed = movementData.moveSpeed;

        stateMachine.StartCoroutine(ProcessMovement());
    }
    protected override void OnUpdateState()
    {
        HandleMovement();
    }

    protected override void OnExitState()
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

    private IEnumerator ProcessMovement()
    {
        NavMeshAgent agent = movementData.agent;

        currentPathObj = movementData.paths[movementData.currentPathIndex];

        //set agent to move to target path
        agent.SetDestination(currentPathObj.transform.position);

        //wait till reaches destination
        while (agent.pathPending || agent.remainingDistance > agent.stoppingDistance)
        {
            yield return null;
        }

        //when path is reached, index it. loop if it exceeds.
        movementData.currentPathIndex++;
        if (movementData.currentPathIndex >= movementData.paths.Count)
        {
            movementData.currentPathIndex = 0;
        }

        NPCStateMachine npcStateMachine = stateMachine as NPCStateMachine;
        stateMachine.ChangeState(npcStateMachine.IdleState);
    }

    private void HandleMovement()
    {
        UpdateRotation();
    }

    private void UpdateRotation()
    {
        if (movementData.agent.velocity != Vector3.zero)
        {
            //targetRotation based on direction of movement of agent
            Quaternion targetRotation = Quaternion.LookRotation(movementData.agent.velocity.normalized);

            movementData.agent.transform.rotation = Quaternion.Slerp(
                movementData.agent.transform.rotation,
                targetRotation,
                Time.deltaTime * movementData.rotationSpeed
            );
        }
    }
}
