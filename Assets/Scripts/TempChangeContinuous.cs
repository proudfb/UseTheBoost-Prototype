using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempChangeContinuous : AbsContinuousEnvironment {
    /// <summary>
    /// How much does the heat change each second?
    /// </summary>
    public float HeatChange = 1;

    protected override void EnvEffect(Rigidbody r) {
        r.GetComponentInParent<HeatGenerationController>().ChangeHeat(HeatChange*.02f);
    }
}
