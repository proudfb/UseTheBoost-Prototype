using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitchAirControl : BasicAbility_ab {

    [SerializeField] private float pitchControlScalar=30;
    private TerrainCheck tcheck;
    Rigidbody car;

    void Start()
    {
        tcheck = gameObject.GetComponent<TerrainCheck>();
        car = gameObject.GetComponent<Rigidbody>();
    }

    protected override void Ability()
    {
        //Debug.Log("AM I FLYING?");
        if (tcheck.IsFlying)
        {
            //Debug.Log("ROTATE!");
            car.AddRelativeTorque(Input.GetAxis(AxisName) * pitchControlScalar * Time.deltaTime, 0, 0, ForceMode.Acceleration); 
        }
    }
}
