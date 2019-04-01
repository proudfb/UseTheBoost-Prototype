using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static int NumberOfCheckpointGates;
    public static int HighestCheckpoint;
    private static List<int> Checkpoints;

    private void Awake() {
        NumberOfCheckpointGates = 0;
        HighestCheckpoint = 0;
        Checkpoints = new List<int>();
    }

    private void Start() {
        ////verify the map is beatable by walking through the list
        //for (int i = 0; i <= HighestCheckpoint; i++) {
        //    if (Checkpoints[i] < 1) {//if we do not have at least one checkpoint for each int from 0 to (highest checkpoint), error out
        //        throw new System.Exception("The map does not have the correct amount or order of checkpoints. Check each checkpoint's rank, and make sure you have enough.");
        //    }
        //}
        Debug.Log("Number of checkpoint gates is " + NumberOfCheckpointGates);
        Debug.Log("Highest checkpoint gate is " + HighestCheckpoint);

    }

    public static void AddCheckpoint(int CheckpointNumber) {
        NumberOfCheckpointGates++;
        Debug.Log("Checkpoint "+ CheckpointNumber + " reporting.");
        if (HighestCheckpoint<CheckpointNumber) {
            HighestCheckpoint = CheckpointNumber;
        }
        //Checkpoints[CheckpointNumber]++;
    }
}
