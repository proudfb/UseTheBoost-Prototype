using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour {
    public string message = "";

    public Text messageField;

    private void OnTriggerEnter(Collider other) {
        if (other.transform.root.gameObject.tag == "Player" && messageField!=null) {
            messageField.text = message;
        }
    }
}
