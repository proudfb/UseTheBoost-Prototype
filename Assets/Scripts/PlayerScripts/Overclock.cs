using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//As a special ability, Overclock is in the Ability3 Slot
public class Overclock : LongAbility_ab {

    [SerializeField] private float overclockFactor = 10;

    protected override void Ability() {
        car.AddRelativeForce(Vector3.forward * overclockFactor * Time.deltaTime, ForceMode.Acceleration);
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }
}
