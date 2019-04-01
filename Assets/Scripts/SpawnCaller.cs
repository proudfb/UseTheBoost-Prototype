using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int position = int.Parse(gameObject.name.Substring(10));
        GameController.spawnPoints[position] = this.gameObject;
    }
}
