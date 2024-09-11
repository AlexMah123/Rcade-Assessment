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
        staminaComponent = dogMovementData.staminaComponent;
    }

    protected override void OnEnterState()
    {
        //set animation
        animator.SetInteger("State", 1);

        // allow movement
        movementData.agent.isStopped = false;
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

        DogStateMachine dogStateMachine = stateMachine as DogStateMachine;
        stateMachine.ChangeState(dogStateMachine.IdleState);
    }

    private void HandleMovement()
    {
        HandleStaminaComponent();
        UpdateRotation();
    }

    private void HandleStaminaComponent()
    {
        //if stamina doesnt exist, use default speed
        if (staminaComponent != null)
        {
            //if not recharging and stamina isnt zero
            if (staminaComponent.rechargeCoroutine == null && staminaComponent.currentStamina > 0)
            {
                staminaComponent.DecreaseEnergy(staminaComponent.depleteRate * Time.deltaTime);
                movementData.agent.speed = movementData.sprintSpeed;

                animator.SetInteger("State", 2);
            }
            else
            {
                movementData.agent.speed = movementData.moveSpeed;

                animator.SetInteger("State", 1);
            }
        }
        else
        {
            //defaulted if no stamina component
            movementData.agent.speed = movementData.moveSpeed;

            animator.SetInteger("State", 1);
        }
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
