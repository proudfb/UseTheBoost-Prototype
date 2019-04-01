using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsContinuousEnvironment : AbsEnvironment
{
    protected override void OnTriggerEnter(Collider other) {
        //this space left intentionally blank
    }

    protected void OnTriggerStay(Collider other) {
        EnvEffect(other.attachedRigidbody);
    }

}
