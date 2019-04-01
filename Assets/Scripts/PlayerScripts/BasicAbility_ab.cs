using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BasicAbility_ab : MonoBehaviour
{
    //ovverride the activate ability and activate cooldown methods, since inherent abilities don't have either. ActivateAbility is now an alias for Ability()

    /// <summary>
    /// The name of the axis to use for this input (key, button, etc)
    /// </summary>
    [SerializeField] protected string axisName;
    /// <summary>
    /// The name of this ability to show in the UI
    /// </summary>
    [SerializeField] protected string abilityName;

    public string AxisName { get { return axisName; } }
    public string AbilityName { get { return abilityName; } }

    public virtual void ActivateAbility()
    {
        Ability();
    }

    /// <summary>
    /// A method that defines what the ability will do. ActivateAbility() handles the abilityIsActive fields and activating the cooldown, in long and standard abilities.
    /// For LongAbilities, this is called every frame "abilityIsActive" is true
    /// </summary>
    protected abstract void Ability();
}
