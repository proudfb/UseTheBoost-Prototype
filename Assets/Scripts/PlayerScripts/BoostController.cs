using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : Ability_ab {
    //As a mobility option, Boost is always in the Ability1 slot
    private HeatGenerationController heatGen;
    private AudioSource rocketBoost;
    [SerializeField] private float BoostForce = 20;
    Rigidbody car;

    new void Start()
    {
        //get the reference to our heat generator controller
        GameObject temp = GameObject.FindGameObjectWithTag("Ability1");
        if (temp == null) {
            throw new System.Exception("No Ability1 slot found in UI");
        }
        UICoolDown = temp.GetComponent<UnityEngine.UI.Image>();

        if (UICoolDown == null) {
            throw new System.Exception("Ability1 doesn't have an image associated with it.");
        }

        base.Start();

        heatGen = gameObject.GetComponent<HeatGenerationController>();
        rocketBoost = gameObject.GetComponent<AudioSource>();
        car = gameObject.GetComponent<Rigidbody>();
    }

    protected override void Ability()
    {
        //ROCKETBOOST YEAH
        car.AddRelativeForce(Vector3.forward * BoostForce, ForceMode.VelocityChange);
        //AUDIO YEAH
        rocketBoost.Play();
        //OVERHEAT YEAH
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }
}
