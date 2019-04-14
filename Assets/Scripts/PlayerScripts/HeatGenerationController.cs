﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatGenerationController : MonoBehaviour
{
    public float Heat { get; protected set; }

    public float startingHeat = 90;

    public Slider heatSlider;

    private PlayerCheckpointTracker pct;

    /// <summary>
    /// The rate of heat generation, in heat buildup/second
    /// </summary>
    public float heatRate;

    /// <summary>
    /// Basic set, but with reference to UI
    /// </summary>
    /// <param name="amount">The amount to change the heat by</param>
    /// <param name="factor">The factor to multiply the heat change, used for difficulty levels</param>
    public void ChangeHeat(float amount, float factor = 1) {
        Heat += amount;
        heatSlider.value = Heat;
        if (Heat < heatSlider.minValue) {
            pct.Respawn(PlayerCheckpointTracker.DEATH_BY_FREEZING);
        }
        if (Heat > heatSlider.maxValue) {
            pct.Respawn(PlayerCheckpointTracker.DEATH_BY_HEAT);
        }
    }

    public void ResetHeat() {
        Debug.Log("Reseting heat");
        Heat = startingHeat;
    }

    private void Start() {
        GameObject temp = GameObject.FindGameObjectWithTag("HeatSlider");
        if (temp != null) {
            heatSlider = temp.GetComponent<Slider>();
        } else throw new System.Exception("There is no heat slider!");
        if (heatSlider == null) {
            throw new System.Exception("There is no heat slider!");
        }

        pct = GetComponentInParent<PlayerCheckpointTracker>();

        Heat = startingHeat;
    }

    private void FixedUpdate() {
        ChangeHeat(heatRate * .02f);
    }
}
