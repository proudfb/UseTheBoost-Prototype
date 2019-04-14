using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Ability_ab {
    //As a mobility option, Boost is always in the Ability1 slot
    [SerializeField] private AudioClip rocketBoost;
    [SerializeField] private float BoostForce = 20;
    private AudioSource audiosource;

    new void Start() {
        base.Start();
        audiosource = gameObject.AddComponent<AudioSource>() as AudioSource;
    }

    protected override void Ability() {
        //ROCKETBOOST YEAH
        car.AddRelativeForce(Vector3.forward * BoostForce, ForceMode.VelocityChange);
        //AUDIO YEAH
        audiosource.PlayOneShot(rocketBoost);
        //OVERHEAT YEAH
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }
}
