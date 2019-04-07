using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shunt : Ability_ab {
    private TerrainCheck tcheck;

    protected override void Ability() {
        if (tcheck.IsFlying) {
            car.AddRelativeForce(Input.GetAxis(AxisName), 0, 0, ForceMode.VelocityChange);
        }
    }

    // Start is called before the first frame update
    new void Start() {
        base.Start();
        tcheck = gameObject.GetComponent<TerrainCheck>();
    }
}
