using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//As a special ability, Overclock is in the Ability3 Slot
public class OverclockController : LongAbility_ab {

    [SerializeField] private float overclockFactor=10;
    private HeatGenerationController heatGen;

    new void Start() {
        GameObject temp = GameObject.FindGameObjectWithTag("Ability3");
        if (temp == null) {
            throw new System.Exception("No Ability3 slot found in UI");
        }
        UICoolDown = temp.GetComponent<UnityEngine.UI.Image>();

        if (UICoolDown == null) {
            throw new System.Exception("Ability3 doesn't have an image associated with it.");
        }

        base.Start();
        //get the reference to our heat generator controller
        heatGen = gameObject.GetComponent<HeatGenerationController>();
    }

    public override void ActivateAbility()
    {
        this.abilityIsActive = true;
        this.abilityTimeLeft = abilityDuration;//set the duration
    }

    protected override void Ability()
    {
        gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * overclockFactor * Time.deltaTime, ForceMode.Acceleration);
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }
}
