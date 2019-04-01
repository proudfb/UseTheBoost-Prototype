using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityController : MonoBehaviour {

    //The lists of abilities that we need to watch for. initialized at runtime.
    private List<Ability_ab> abilities = new List<Ability_ab>();
    private List<LongAbility_ab> longAbilities = new List<LongAbility_ab>();
    private List<BasicAbility_ab> basicAbilities = new List<BasicAbility_ab>();

	// Called after all the Start() functions are called.
	void Awake () {
        foreach (BasicAbility_ab ability in gameObject.GetComponents<BasicAbility_ab>()){
            if (ability is LongAbility_ab)//if the ability is a long ability
            {
                longAbilities.Add(ability as LongAbility_ab);//add it to our list of long abilities
            }
            else if (ability is Ability_ab)//if our ability has a cooldown, add it to the respective list
            {
                abilities.Add(ability as Ability_ab);
            }
            else //our ability is an inherent ability, so add it to the respective list
            {
                basicAbilities.Add(ability);
            }
        }
	}
	
	void FixedUpdate () {
        foreach (Ability_ab ability in abilities)
        {
            if (Input.GetButtonDown(ability.AxisName))
            {
                ability.ActivateAbility();
            }
        }

        foreach (BasicAbility_ab Bability in basicAbilities)
        {
            //Debug.Log("The axis I'm geting is called: " + Bability.AxisName);
            if (Input.GetAxis(Bability.AxisName) != 0)
            {
                //Debug.Log("SHOULD BE ROTATING!");
                Bability.ActivateAbility();
            }
        }

        foreach (LongAbility_ab lAbility in longAbilities)
        {
            if (Input.GetButtonDown(lAbility.AxisName))
            {
                lAbility.ToggleAbility();
            }
        }

        //Overclock
        //Debug.Log("Overclock key is signalling:"+Input.GetAxis("Fire1").ToString());
        //if (Input.GetKey(KeyCode.V))
        //{
        //    //gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * OverclockAccel * Time.deltaTime, ForceMode.Acceleration);
        //    //heatGen.ChangeHeat(.125f, heatScalar);
        //}
    }
}
