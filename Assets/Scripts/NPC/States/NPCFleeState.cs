using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFleeState : BaseState
{
    //runtime value
    private GameObject currentPathObj;

    //injected values
    private MovementData movementData;
    private float fleeDistance;

    Vector3 fleeDirection;
    GameObject fleeTarget;

    public NPCFleeState(BaseStateMachine stateMachine, Animator animator, NPCMovementData npcMovementData) : base(stateMachine, animator)
    {
        fleeDistance = npcMovementData.fleeDistance;
        movementData = npcMovementData.movementData;
    }

    protected override void OnEnterState()
    {
        //set animation 
        animator.SetInteger("State", 2);

        // allow movement
        movementData.agent.isStopped = false;
        movementData.agent.speed = movementData.moveSpeed / 2;

        ProcessFleeMovement();
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
        if(other.gameObject.CompareTag("FleeTarget"))
        {
            fleeTarget = other.gameObject;
        }
    }

    public override void OnTriggerExit(Collider other)
    {

    }

    private void HandleMovement()
    {
        UpdateRotation();
    }


    private void ProcessFleeMovement()
    {
        NavMeshAgent agent = movementData.agent;

        currentPathObj = movementData.paths[movementData.currentPathIndex];

        Vector3 targetPosition = fleeTarget != null ? fleeTarget.transform.position : currentPathObj.transform.position;
        Vector3 currentPosition = agent.transform.position;
        fleeDirection = (currentPosition - targetPosition).normalized;
        Vector3 fleeDestination = currentPosition + fleeDirection * fleeDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(fleeDestination, out hit, 5.0f, NavMesh.AllAreas))
        {
            // Set the agent's destination to the closest valid point
            agent.SetDestination(hit.position);
        }
    }


    private void UpdateRotation()
    {
        if (movementData.agent.velocity != Vector3.zero)
        {
            //targetRotation based on direction of movement of agent
            Quaternion targetRotation = Quaternion.LookRotation(-fleeDirection);

            movementData.agent.transform.rotation = Quaternion.Slerp(
                movementData.agent.transform.rotation,
                targetRotation,
                Time.deltaTime * movementData.rotationSpeed
            );
        }
    }

 
}
