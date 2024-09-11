using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogPatrolState : BaseState
{
    //runtime value
    private GameObject currentPathObj;

    //injected values
    private MovementData movementData;
    private StaminaComponent staminaComponent;

    public DogPatrolState(BaseStateMachine stateMachine, Animator animator, DogMovementData dogMovementData) 
        : base(stateMachine, animator)
    {
        movementData = dogMovementData.movementData;
    }

    protected override void OnEnterState()
    {
        //set animation
        animator.SetInteger("State", 1);

        stateMachine.StartCoroutine(ProcessMovement());
    }

    protected override void OnUpdateState()
    {
        HandleMovement();
    }

    protected override void OnExitState()
    {
        // Stop movement
        movementData.agent.isStopped = true;
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

        DogStateMachine dogStateMachine = stateMachine as DogStateMachine;
        stateMachine.ChangeState(dogStateMachine.IdleState);
    }

    private void HandleMovement()
    {
        //if stamina doesnt exist, use default speed
        if (staminaComponent != null && staminaComponent.currentStamina > 0)
        {
            movementData.agent.speed = movementData.sprintSpeed; // Assuming you have sprintSpeed in MovementData
        }
        else
        {
            movementData.agent.speed = movementData.moveSpeed;
        }

        if (movementData.agent.velocity != Vector3.zero)
        {
            //targetRotation based on movement of agent
            Quaternion targetRotation = Quaternion.LookRotation(movementData.agent.velocity.normalized);

            movementData.agent.transform.rotation = Quaternion.Slerp(
                movementData.agent.transform.rotation,
                targetRotation,
                Time.deltaTime * movementData.rotationSpeed
            );
        }

        movementData.agent.speed = movementData.moveSpeed;
    }
}
