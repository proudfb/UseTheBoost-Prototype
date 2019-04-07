using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Ability_ab {
    //As a mobility option, Boost is always in the Ability1 slot
    private AudioSource rocketBoost;
    [SerializeField] private float BoostForce = 20;

    new void Start() {
        base.Start();
        rocketBoost = gameObject.GetComponent<AudioSource>();
    }

    protected override void Ability() {
        //ROCKETBOOST YEAH
        car.AddRelativeForce(Vector3.forward * BoostForce, ForceMode.VelocityChange);
        //AUDIO YEAH
        rocketBoost.Play();
        //OVERHEAT YEAH
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }
}
