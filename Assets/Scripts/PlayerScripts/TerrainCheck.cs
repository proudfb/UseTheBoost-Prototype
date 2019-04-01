using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCheck : MonoBehaviour
{

    RaycastHit lastHit;
    GameObject parentObj;

    HeatGenerationController heatGen;

    private bool isFlying = false;
    private float TimeStartedFlying;
    private float TimeStoppedFlying;

    //declaring these once to help performance. Save the frames!
    private float xRot, yRot, zRot, pXRot, pYRot, pZRot, tRot;

    /// <summary>
    /// Scalar to multiply airtime by, which is then used to reduce heat
    /// </summary>
    public float AirCoolingFactor = 10;

    public bool IsFlying { get { return isFlying; } }

    // Use this for initialization
    void Start()
    {
        parentObj = this.gameObject;
        heatGen = gameObject.GetComponent<HeatGenerationController>();
        Debug.Log("Attached to " + parentObj.ToString());
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(parentObj.transform.position, parentObj.transform.TransformVector(Vector3.down), out lastHit, .5f)) //if we hit something
        {
            if (isFlying)
            {
                //we aren't flying anymore
                isFlying = false;
                Debug.Log("Not Flying");
                //get the time we stopped flying
                TimeStoppedFlying = Time.time;
                //if the last time we landed didn't happen in the last 10 fixed time steps (1/5 of a second)
                if (TimeStoppedFlying - TimeStartedFlying > .2)
                {
                    Debug.Log("Heat reduced by " + ((TimeStartedFlying - TimeStoppedFlying) * AirCoolingFactor).ToString());
                    heatGen.ChangeHeat((TimeStartedFlying - TimeStoppedFlying), AirCoolingFactor);
                    //when we hit the ground for the first time during this landing
                    if (lastHit.collider.gameObject.tag == "Terrain")
                    {
                        ////Debug.Log("On the Ground!");

                        tRot = 0;

                        //get the rotation of the object we hit
                        xRot = lastHit.collider.gameObject.transform.rotation.eulerAngles.x;
                        yRot = lastHit.collider.gameObject.transform.rotation.eulerAngles.y;
                        zRot = lastHit.collider.gameObject.transform.rotation.eulerAngles.z;


                        //then find our rotation
                        pXRot = parentObj.transform.rotation.eulerAngles.x;
                        pYRot = parentObj.transform.rotation.eulerAngles.y;
                        pZRot = parentObj.transform.rotation.eulerAngles.z;

                        //now find the difference between all of them
                        tRot += Mathf.Abs(xRot - pXRot);
                        tRot += Mathf.Abs(yRot - pYRot);
                        tRot += Mathf.Abs(zRot - pZRot);

                        //if the difference between all of them is less than 20, we've made a good landing
                        if (tRot < 20)
                        {
                            Debug.Log("Nice Landing!");
                            heatGen.ChangeHeat(10, -1);
                        }
                    }
                }
            }
        }
        else
        {
            //We're flying
            Debug.Log("Flying");
            if (!isFlying)
            {
                TimeStartedFlying = Time.time;
                isFlying = true;
            }
        }

    }
}
