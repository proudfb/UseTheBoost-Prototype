using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//As a special ability, Cooldown is in the Ability3 slot
public class CooldownAbController : LongAbility_ab
{
    private HeatGenerationController heatGen;
    Rigidbody car;
    public override void ActivateAbility() {
        this.abilityIsActive = true;//set the ability to "active"
        this.abilityTimeLeft = abilityDuration;//set the duration
    }

    protected override void Ability() {
        car.AddRelativeForce(-(car.velocity / 2), ForceMode.Acceleration);
        heatGen.ChangeHeat(-heatGenerated, (1/heatScalar));
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
