using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideController : LongAbility_ab
{
    private HeatGenerationController heatGen;
    private const float GRAVITY = 9.8f;
    /// <summary>
    /// How much should we push up on the car?
    /// </summary>
    [SerializeField] private float antiGravScale = .5f;

    public override void ActivateAbility() {
        this.abilityIsActive = true;//set the ability to "active"
        this.abilityTimeLeft = abilityDuration;//set the duration
    }

    /// <summary>
    /// Push up on the car, extending airtime.
    /// </summary>
    protected override void Ability() {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*(GRAVITY*antiGravScale), ForceMode.Force);
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }

    // Start is called before the first frame update
    void Start()
    {
        heatGen = gameObject.GetComponent<HeatGenerationController>();
    }
}
