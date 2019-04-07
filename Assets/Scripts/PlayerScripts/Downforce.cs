using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Downforce : LongAbility_ab {

    [SerializeField] private float downforceFactor = 10;

    protected override void Ability() {
        car.AddRelativeForce(Vector3.down * downforceFactor, ForceMode.Impulse);
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }
}
