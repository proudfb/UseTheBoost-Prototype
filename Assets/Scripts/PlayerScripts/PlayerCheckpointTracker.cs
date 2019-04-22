using Statics;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Track checkpoints and time for each player, and implement respawning behaviours.
/// </summary>
public class PlayerCheckpointTracker : MonoBehaviour {
    //Constants
    public const int DEATH_BY_HEAT = 1;
    public const int DEATH_BY_FREEZING = 2;
    public const int DEATH_BY_ZONE_OUT = 3;
    public const int DEATH_BY_COMMAND = 127;
    const string TIMEFORMAT = "{0:mm\\:ss\\.fff}";

    public GameController gameController;

    /// <summary>
    /// Fires on respawn of the player. Happens before translocation and force nullification.
    /// </summary>
    public UnityEvent respawn;

    //UI
    public Text LapCounter;
    public Text CheckpointCounter;
    public Text Timer;
    private TimeSpan innerTimer;
    private bool race = false;

    private float[] timestamps;

    private Rigidbody car;
    private HeatGenerationController heatgen;

    //Properties
    public int CurrentCheckpoint { get; private set; }
    public Quaternion LastCheckpointRot { get; private set; }
    public Vector3 LastCheckpointPos { get; private set; }
    public int CurrentLap { get; private set; }


    private void Awake() {
        if (respawn==null) {
            respawn = new UnityEvent();
        }
    }
    // Start is called before the first frame update
    void Start() {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        //Get references to UI stuff
        GameObject temp1, temp2, temp3;
        temp1 = GameObject.FindGameObjectWithTag("CheckpointMonitor");
        temp2 = GameObject.FindGameObjectWithTag("LapMonitor");
        temp3 = GameObject.FindGameObjectWithTag("Timer");

        if (temp1 == null || temp2 == null) {
            throw new System.Exception("A lap or checkpoint monitor wasn't found.");
        }
        if (temp3==null) {
            throw new System.Exception("The timer wasn't found.");
        }

        CheckpointCounter = temp1.GetComponent<Text>();
        LapCounter = temp2.GetComponent<Text>();
        Timer = temp3.GetComponent<Text>();

        if (CheckpointCounter == null || LapCounter == null) {
            throw new System.Exception("A lap or checkpoint monitor didn't have a text component.");
        }

        if (Timer == null) {
            throw new System.Exception("The timer doesn't have a text component.");
        }

        //Set up UI stuff
        CurrentLap = 0;
        CurrentCheckpoint = CheckpointManager.HighestCheckpoint;

        LapCounter.text = string.Format("Lap {0}/{1}", CurrentLap, GameController.NumberOfLaps);
        CheckpointCounter.text = string.Format("Checkpoint: Start/{0}", CheckpointManager.HighestCheckpoint);

        timestamps = new float[GameController.NumberOfLaps + 1];
        innerTimer = TimeSpan.Zero;
        Timer.text = string.Format(TIMEFORMAT,innerTimer.ToString());
        
        //set up heatController and car stuff.
        heatgen = this.GetComponentInParent<HeatGenerationController>();
        car = this.GetComponentInParent<Rigidbody>();

    }

    private void Update() {
        if (Input.GetButtonDown("Respawn") && !PlayerInfo.IsCountDown) {//if we hit the respawn key
            Respawn(DEATH_BY_COMMAND);//respawn
        }
        if (race) {
            innerTimer += TimeSpan.FromSeconds(Time.deltaTime);
            Timer.text = string.Format(TIMEFORMAT, innerTimer); 
        }
    }

    public void UpdateCheckpoint(int checkpointNumber, Vector3 checkPos, Quaternion checkRot, AudioSource soundCue = null) {
        if (checkpointNumber == (CurrentCheckpoint + 1)) {
            //since the checkpoint is is sequence, update it.
            CheckpointHelper(checkpointNumber, checkPos, checkRot, soundCue);
        }
        if (checkpointNumber == 0 && CurrentCheckpoint == (CheckpointManager.HighestCheckpoint)) {
            //if the checkpoint is the finishline, do stuff for that instead.
            //if the race hasn't started yet, do so.
            if (timestamps[0]==0f) {
                race = true;
            }
            //update the timestamp for the last lap
            timestamps[CurrentLap] = Time.time;
            CurrentLap++;
            CheckpointHelper(checkpointNumber, checkPos, checkRot, soundCue);
            LapCounter.text = string.Format("Lap {0}/{1}", CurrentLap, GameController.NumberOfLaps);

            if (CurrentLap > GameController.NumberOfLaps) {//if we finished the last lap,
                //end the race
                race = false;
                gameController.FinishRace(timestamps);//finish the race
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
        Debug.Log("Respawning");
        respawn.Invoke();
        transform.root.SetPositionAndRotation(LastCheckpointPos + Vector3.up, LastCheckpointRot);
        car.velocity = Vector3.zero;
        car.angularVelocity = Vector3.zero;
    }

    public void Respawn(int deathFlag) {
        switch (deathFlag) {
            case DEATH_BY_HEAT:
                Debug.Log("Player was too hot.");
                heatgen.ResetHeat();
                break;
            case DEATH_BY_FREEZING:
                Debug.Log("Player was too cool.");
                heatgen.ResetHeat();
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
