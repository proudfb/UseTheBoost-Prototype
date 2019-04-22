using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : Ability_ab {
    //As a mobility option, Boost is always in the Ability1 slot
    [SerializeField] private AudioClip rocketBoost;
    [SerializeField] private float BoostForce = 20;
    public AudioSource audiosource;
    private PlayerCheckpointTracker pct;

    new void Start() {
        base.Start();
        audiosource = gameObject.AddComponent<AudioSource>() as AudioSource;
        pct = gameObject.GetComponent<PlayerCheckpointTracker>();
        pct.respawn.AddListener(ResetAudio);
    }

    protected override void Ability() {
        //ROCKETBOOST YEAH
        car.AddRelativeForce(Vector3.forward * BoostForce, ForceMode.VelocityChange);
        //AUDIO YEAH
        audiosource.PlayOneShot(rocketBoost);
        //OVERHEAT YEAH
        heatGen.ChangeHeat(heatGenerated, heatScalar);
    }
    private void OnDestroy() {
        pct.respawn.RemoveListener(ResetAudio);
    }

    //just to fix a weird bug that only sometimes happens
    private void ResetAudio() {
        if (audiosource==null) {
            audiosource = gameObject.AddComponent<AudioSource>() as AudioSource; 
        }
    }
}
