using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public int CheckpointNumber;
    private AudioSource soundCue;
    // Start is called before the first frame update

    void Awake() {
        soundCue = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        CheckpointManager.AddCheckpoint(CheckpointNumber);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.transform.root.gameObject.tag=="Player") {
            other.GetComponentInParent<PlayerCheckpointTracker>().UpdateCheckpoint(this.CheckpointNumber, this.transform.position, this.transform.rotation, this.soundCue);
        }
    }
}
