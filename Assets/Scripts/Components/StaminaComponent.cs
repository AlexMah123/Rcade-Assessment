using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaComponent : MonoBehaviour
{
    [Header("Stamina Config")]
    public float currentStamina;
    public float maxStamina;
    public float rechargeRate;
    public float depleteRate;

    public Coroutine rechargeCoroutine;

    //declaration of events
    public event Action<float> OnStaminaModified;

    private void Awake()
    {
        currentStamina = maxStamina;
    }

    #region exposed methods
    public void IncreaseEnergy(float value)
    {
        ModifyStaminaAmount(value);
    }

    public void DecreaseEnergy(float value)
    {
        ModifyStaminaAmount(-value);

        //handles recharging logic
        if(currentStamina <= 0)
        {
            currentStamina = 0;

            if(rechargeCoroutine != null)
            {
                StopCoroutine(rechargeCoroutine);
            }

            rechargeCoroutine = StartCoroutine(RechargeStamina());
        }
    }

    public void HandleRechargeStamina()
    {
        rechargeCoroutine = StartCoroutine(RechargeStamina());
    }

    public void StopRechargeStamina()
    {
        if(rechargeCoroutine != null)
        {
            StopCoroutine(rechargeCoroutine);
            rechargeCoroutine = null;
        }
    }

    #endregion


    #region Internal Function
    private void ModifyStaminaAmount(float value)
    {
        currentStamina = Mathf.Clamp(currentStamina + value, 0, maxStamina);

        OnStaminaModified?.Invoke(currentStamina);
    }

    private IEnumerator RechargeStamina()
    {
        while (currentStamina < maxStamina)
        {
            IncreaseEnergy(rechargeRate * Time.deltaTime);
            yield return null;
        }

        currentStamina = maxStamina;
        rechargeCoroutine = null;
        OnStaminaModified?.Invoke(currentStamina);
    }
    #endregion
}
