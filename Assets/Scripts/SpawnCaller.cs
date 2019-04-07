using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //get the number after "SpawnPoint" in an object's name
        int position = int.Parse(gameObject.name.Substring(10));
        GameController.spawnPoints[position] = this.gameObject;
    }
}
