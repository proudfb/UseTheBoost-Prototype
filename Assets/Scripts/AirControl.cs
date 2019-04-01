using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirControl : MonoBehaviour
{
    private GameObject parentObj;
    private RaycastHit lastHit;
    private bool isFlying = false;
    [SerializeField] private float PitchControlScalar;
    [SerializeField] private string PitchAxisName;
    [SerializeField] private string RollAxisName;
    [SerializeField] private float RollControlScalar;

    void Start() {
        parentObj = this.gameObject;
        Debug.Log("Attached to " + parentObj.ToString());
    }

    private void FixedUpdate() {
        if (Physics.Raycast(parentObj.transform.position, parentObj.transform.TransformVector(Vector3.down), out lastHit, .5f)) {
            //we were flying, but now we are not.
            if (isFlying) {
                isFlying = false;
            } 
        } else {
            //we are flying (or have rolled over), because we didn't hit anything
            if (!isFlying) { isFlying = true; } //if we weren't flying, we are now.
            gameObject.GetComponent<Rigidbody>().AddRelativeTorque(Input.GetAxis(PitchAxisName) * PitchControlScalar * Time.deltaTime, 0, 0, ForceMode.Acceleration);
            gameObject.GetComponent<Rigidbody>().AddRelativeTorque(0, 0, -(Input.GetAxis(RollAxisName) * RollControlScalar * Time.deltaTime), ForceMode.Acceleration);
        }
    }
}
