using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovementData
{
    public MovementData movementData;
    public float waitTimeAtPath;
    public StaminaComponent staminaComponent;

    public DogMovementData(MovementData movementData, StaminaComponent staminaComponent, float waitTimeAtPath)
    {
        this.movementData = movementData;
        this.staminaComponent = staminaComponent;
        this.waitTimeAtPath = waitTimeAtPath;
    }
}

[RequireComponent(typeof(MovementComponent))]
public class DogStateMachine : BaseStateMachine
{
    [Header("Dog Configs")]
    [SerializeField] float waitTimeAtPath = 3f;

    private MovementComponent movementComponent;
    private StaminaComponent staminaComponent; //can be null, it will just use default speed

    #region States
    public DogIdleState IdleState;
    public DogPatrolState PatrolState;
    #endregion

    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movementComponent = GetComponent<MovementComponent>();
        staminaComponent = GetComponent<StaminaComponent>();
    }


    private void Start()
    {
        InitializeData();
        InitializeStateMachine(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void InitializeData()
    {
        if (movementComponent == null)
        {
            Debug.LogError("MovementComponent is not attached");
        }

        DogMovementData dogMovementInfo = new(movementComponent.movementData, staminaComponent, waitTimeAtPath);

        IdleState = new(this, animator, dogMovementInfo);
        PatrolState = new(this, animator, dogMovementInfo);
    }
}
