using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsEnvironment : MonoBehaviour
{

    private string effectName;
    private float baseCooldown;
    private float cooldown = 0;
    private bool isReady = false;

    public string EffectName { get => effectName; protected set => effectName = value; }
    /// <summary>
    /// The cooldown before this effect can fire again, in fixed time units
    /// </summary>
    public float BaseCooldown { get => baseCooldown; protected set => baseCooldown = value; }
    /// <summary>
    /// The cooldown time remaining
    /// </summary>
    public float Cooldown { get => cooldown; protected set => cooldown = value; }
    /// <summary>
    /// Is the object ready to fire its effect?
    /// </summary>
    public bool IsReady { get => isReady; set => isReady = value; }

    /// <summary>
    /// The effect to use when the object is triggered.
    /// </summary>
    protected abstract void EnvEffect(Rigidbody r);

    /// <summary>
    /// Trigger system for the object. AbsContinuousEnvironment overrides this and implements OnTriggerStay instead.
    /// </summary>
    /// <param name="other">The other object that hit this collider</param>
    protected virtual void OnTriggerEnter(Collider other) {
        EnvEffect(other.attachedRigidbody);
    }
}
