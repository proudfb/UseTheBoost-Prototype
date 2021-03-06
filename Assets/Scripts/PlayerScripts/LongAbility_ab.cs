﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Defines an ability that lasts for more than one frame. Inherits from AbilityAbstract
/// </summary>
public abstract class LongAbility_ab : Ability_ab {
    /// <summary>
    /// How long does this ability last, in seconds?
    /// </summary>
    [Tooltip("How long does this ability last, in seconds?")]
    [SerializeField] protected float abilityDuration;
    //[SerializeField] protected Text debug_Status;
    [SerializeField] protected Sprite ActiveIcon;

    protected float abilityTimeLeft = 0f;

    /// <summary>
    /// Activate the ability. Don't forget to set the "AbilityIsActive" value to true if overriding!
    /// </summary>
    public override void ActivateAbility() {
        this.abilityIsActive = true;
        this.abilityTimeLeft = abilityDuration;//set the duration
        UICoolDown.sprite = ActiveIcon;
    }

    protected override void ActivateCooldown() {
        UICoolDown.sprite = CoolDownIcon;
        base.ActivateCooldown();

    }

    public void ToggleAbility() {
        if (this.abilityIsActive) {
            abilityIsActive = false;
            abilityTimeLeft = 0f;
            ActivateCooldown();
        } else //abilityIsActive is false
          {
            if (abilityIsReady) {
                ActivateAbility();
            }
        }
    }

    private void FixedUpdate() {
        //timerText.text = string.Format("{0:S} ready in: {1:F3} seconds", abilityName, Cooldown());
        //debug_Status.text = string.Format("{0:S} Ready: {1:S}\n" +
        //    "Active: {2:S}", abilityName, AbilityIsReady, AbilityIsActive);
        Cooldown();
        if (abilityIsActive) {
            Ability();
            if (abilityTimeLeft > 0)//if there is time remaining,
            {
                abilityTimeLeft -= Time.deltaTime;//decrement it so that we don't get infinite abilities.
            } else //no more ability time remaining, shut it down
              {
                ToggleAbility(); //we can use this here because it's guaranteed that the ability is active.
            }
        }
    }
}
