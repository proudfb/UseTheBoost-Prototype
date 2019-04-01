using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvBoostPad : AbsEnvironment
{
    /// <summary>
    /// Boost level. Multiplied by 100 in actual calculations
    /// </summary>
    [SerializeField] private float BoostLevel = 10;

    public float BoostLevel1 { get => BoostLevel; set => BoostLevel = value; }

    protected override void EnvEffect(Rigidbody r)
    {
        r.AddForce(this.transform.TransformVector(Vector3.forward) * BoostLevel /* 10*/, ForceMode.VelocityChange);
    }

}
