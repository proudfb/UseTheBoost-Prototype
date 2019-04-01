using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerCheckpointTracker : MonoBehaviour {
    public const int DEATH_BY_HEAT = 1;
    public const int DEATH_BY_FREEZING = 2;
    public const int DEATH_BY_ZONE_OUT = 3;
    public const int DEATH_BY_COMMAND = 127;

    public GameController gameController;

    public Text LapCounter;
    public Text CheckpointCounter;

    public int CurrentCheckpoint { get; private set; }
    public Quaternion LastCheckpointRot { get; private set; }
    public Vector3 LastCheckpointPos { get; private set; }
    public int CurrentLap { get; private set; }

    // Start is called before the first frame update
    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        //Get references to UI stuff
        GameObject temp1, temp2;
        temp1 = GameObject.FindGameObjectWithTag("CheckpointMonitor");
        temp2 = GameObject.FindGameObjectWithTag("LapMonitor");

        if (temp1 == null || temp2 == null) {
            throw new System.Exception("A lap or checkpoint monitor wasn't found.");
        }

        CheckpointCounter = temp1.GetComponent<Text>();
        LapCounter = temp2.GetComponent<Text>();

        if (CheckpointCounter == null || LapCounter == null) {
            throw new System.Exception("A lap or checkpoint monitor didn't have a text component.");
        }

        //Set up UI stuff
        CurrentLap = 0;
        CurrentCheckpoint = CheckpointManager.HighestCheckpoint;
        LapCounter.text = string.Format("Lap {0}/{1}", CurrentLap, GameController.NumberOfLaps);
        CheckpointCounter.text = string.Format("Checkpoint: Start/{0}", CheckpointManager.HighestCheckpoint);
    }

    private void Update() {
        if (Input.GetButtonDown("Respawn")) {//if we hit the respawn key
            Respawn(DEATH_BY_COMMAND);//respawn
        }
    }

    public void UpdateCheckpoint(int checkpointNumber, Vector3 checkPos, Quaternion checkRot, AudioSource soundCue = null) {
        if (checkpointNumber == (CurrentCheckpoint + 1)) {
            //since the checkpoint is is sequence, update it.
            CheckpointHelper(checkpointNumber, checkPos, checkRot, soundCue);
        }
        if (checkpointNumber == 0 && CurrentCheckpoint == (CheckpointManager.HighestCheckpoint)) {
            //if the checkpoint is the finishline, do stuff for that instead.
            CurrentLap++;
            CheckpointHelper(checkpointNumber, checkPos, checkRot, soundCue);
            LapCounter.text = string.Format("Lap {0}/{1}", CurrentLap, GameController.NumberOfLaps);

            if (CurrentLap > GameController.NumberOfLaps) {//if we finished the last lap,
                gameController.FinishRace();//finish the race
            }
        }
        
    }

    private void CheckpointHelper(int checkpointNumber, Vector3 checkPos, Quaternion checkRot, AudioSource audio = null) {
        if (audio != null && audio.enabled) {
            audio.Play();
        }
        CurrentCheckpoint = checkpointNumber;
        LastCheckpointPos = checkPos;
        LastCheckpointRot = checkRot;
        CheckpointCounter.text = string.Format("Checkpoint: {0}/{1}", CurrentCheckpoint, CheckpointManager.HighestCheckpoint);

    }

    public void Respawn() {
        transform.root.SetPositionAndRotation(LastCheckpointPos + Vector3.up, LastCheckpointRot);
        this.GetComponentInParent<Rigidbody>().velocity = Vector3.zero;
        this.GetComponentInParent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void Respawn(int deathFlag) {
        switch (deathFlag) {
            case DEATH_BY_HEAT:
                Debug.Log("Player was too hot.");
                this.GetComponentInParent<HeatGenerationController>().ResetHeat();
                break;
            case DEATH_BY_FREEZING:
                Debug.Log("Player was too cool.");
                this.GetComponentInParent<HeatGenerationController>().ResetHeat();
                break;
            case DEATH_BY_ZONE_OUT:
                Debug.Log("Player had a falling out.");
                break;
            case DEATH_BY_COMMAND:
                Debug.Log("Player bid farewell, cruel world!");
                break;
            default:
                break;
        }
        Respawn();
    }
}
