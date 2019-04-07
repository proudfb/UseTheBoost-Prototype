using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boost a rigidbody (usually a car) upwards. Jump is an added mobility option, so it's in slot 2
/// </summary>
public class Jump : Ability_ab {
    [SerializeField] private float JumpForce = 2;
    new void Start() {

        base.Start();
    }

    protected override void Ability() {
        car.AddRelativeForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }

}
