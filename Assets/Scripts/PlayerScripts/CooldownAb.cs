using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cool your car down, at the expense of speed. As a special ability, Cooldown is in the Ability3 slot
/// </summary>
public class CooldownAb : LongAbility_ab {
    protected override void Ability() {
        car.AddRelativeForce(car.transform.InverseTransformDirection(0,0,-car.velocity.z*.5f), ForceMode.Acceleration);
        heatGen.ChangeHeat(-heatGenerated, (1 / heatScalar));
    }
}
