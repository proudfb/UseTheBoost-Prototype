using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glide : LongAbility_ab {
    private const float GRAVITY = 9.8f;
    /// <summary>
    /// How much should we push up on the car?
    /// </summary>
    [SerializeField] private float antiGravScale = .5f;

    /// <summary>
    /// Push up on the car, extending airtime.
    /// </summary>
    protected override void Ability() {
        car.AddForce(Vector3.up * (GRAVITY * antiGravScale), ForceMode.Acceleration);
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }
}
