using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaComponent : MonoBehaviour
{
    [Header("Runtime Value")]
    public int staminaAmount;

    //declaration of events
    public event Action<int> OnStaminaModified;

    public void IncreaseEnergy(int value)
    {
        ModifyStaminaAmount(value);
    }

    public void DecreaseEnergy(int value)
    {
        ModifyStaminaAmount(-1 * value);
    }

    #region Internal Function
    private void ModifyStaminaAmount(int value)
    {
        staminaAmount += value;

        OnStaminaModified?.Invoke(staminaAmount);
    }
    #endregion
}
