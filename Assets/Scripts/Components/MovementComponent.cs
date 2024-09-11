using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class MovementData
{
    public NavMeshAgent agent;

    [Header("Movement Config")]
    public float moveSpeed;
    public float sprintSpeed;
    public float rotationSpeed;

    [Header("Path Config")]
    public List<GameObject> paths;
    public int currentPathIndex;

    public MovementData(NavMeshAgent agent, float moveSpeed, float sprintSpeed, float rotationSpeed, List<GameObject> paths, int currentPathIndex)
    {
        this.agent = agent;

        this.moveSpeed = moveSpeed;
        this.sprintSpeed = sprintSpeed;
        this.rotationSpeed = rotationSpeed;

        this.paths = paths;
        this.currentPathIndex = currentPathIndex;
    }
}

[RequireComponent(typeof(NavMeshAgent))]
public class MovementComponent : MonoBehaviour
{
    [Header("Movement Config")]
    public float moveSpeed = 3f;
    public float sprintSpeed = 6f;
    public float rotationSpeed = 10f;

    [Header("Path Config")]
    public List<GameObject> paths;

    //components
    [HideInInspector] public NavMeshAgent agent;

    public MovementData movementData;

    private int defaultPathIndex = 0;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if(paths.Count == 0)
        {
            Debug.LogError($"{gameObject}'s movementComponent has 0 path assigned");
        }

        movementData = new(agent, moveSpeed, sprintSpeed, rotationSpeed, paths, defaultPathIndex);
    }

    private void Start()
    {
        // disable rotation
        movementData.agent.angularSpeed = 0f;
        movementData.agent.updateRotation = false;
    }
}
