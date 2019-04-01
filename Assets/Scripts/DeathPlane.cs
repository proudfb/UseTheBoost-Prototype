using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.transform.root.gameObject.tag=="Player") {
            other.GetComponentInParent<PlayerCheckpointTracker>().Respawn(PlayerCheckpointTracker.DEATH_BY_ZONE_OUT);
        }
    }
}
