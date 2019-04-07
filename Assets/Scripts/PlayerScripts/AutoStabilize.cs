using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Quickly lerp towards no rotational forces. As a special ability, Autostabilize is in the Ability3 Slot
/// </summary>
public class AutoStabilize : LongAbility_ab {

    private HeatGenerationController heatGen;
    Rigidbody car;
    public override void ActivateAbility() {
        this.abilityIsActive = true;//set the ability to "active"
        this.abilityTimeLeft = abilityDuration;//set the duration
    }

    protected override void Ability() {
        car.AddRelativeTorque(Vector3.Lerp(car.angularVelocity, Vector3.zero, .75f));
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }

    // Start is called before the first frame update
    new void Start()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Ability3");
        if (temp == null) {
            throw new System.Exception("No Ability3 slot found in UI");
        }
        UICoolDown = temp.GetComponent<UnityEngine.UI.Image>();

        if (UICoolDown == null) {
            throw new System.Exception("Ability3 doesn't have an image associated with it.");
        }

        base.Start();

        heatGen = gameObject.GetComponent<HeatGenerationController>();
        car = gameObject.GetComponent<Rigidbody>();
    }
}
