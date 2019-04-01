using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines an ability that lasts for more than one frame. Inherits from AbilityAbstract
/// </summary>
public abstract class LongAbility_ab : Ability_ab
{

    /// <summary>
    /// How long does this ability last, in seconds?
    /// </summary>
    [SerializeField] protected float abilityDuration;
    [SerializeField] protected Text debug_Status;


    protected float abilityTimeLeft = 0f;

    //force an override for activate ability, since long abilities have more general requirements than one frame abilities.
    /// <summary>
    /// Activate the ability. Don't forget to set the "AbilityIsActive" value to true!
    /// </summary>
    public abstract override void ActivateAbility();

    public void ToggleAbility()
    {
        if (this.abilityIsActive)
        {
            abilityIsActive = false;
            abilityTimeLeft = 0f;
            ActivateCooldown();
        }
        else //abilityIsActive is false
        {
            if (abilityIsReady)
            {
                ActivateAbility();
            }
        }
    }

    private void FixedUpdate()
    {
        //timerText.text = string.Format("{0:S} ready in: {1:F3} seconds", abilityName, Cooldown());
        //debug_Status.text = string.Format("{0:S} Ready: {1:S}\n" +
        //    "Active: {2:S}", abilityName, AbilityIsReady, AbilityIsActive);
        Cooldown();
        if (abilityIsActive)
        {
            Ability();
            if (abilityTimeLeft > 0)//if there is time remaining,
            {
                abilityTimeLeft -= Time.deltaTime;//decrement it so that we don't get infinite abilities.
            }
            else //no more ability time remaining, shut it down
            {
                ToggleAbility(); //we can use this here because it's guaranteed that the ability is active.
            }
        }
    }
}
