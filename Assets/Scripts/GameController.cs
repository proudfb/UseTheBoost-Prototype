using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int NumberOfLaps = 3;
    public int AltNumberOfLaps;
    public string NextLevel;

    public UnityStandardAssets.Vehicles.Car.CarController debug_Car;

    //private GameObject startingGrid;
    public static GameObject[] spawnPoints;

    private void Awake() {
        //startingGrid = GameObject.Find("StartingGrid");
        if (PlayerInfo.NumberOfPlayers<1) {
            PlayerInfo.NumberOfPlayers = 1;
        }
        if (PlayerInfo.PlayerCars[0] == null) {
            PlayerInfo.PlayerCars[0] = debug_Car;
        }
        //also check PlayerInfo when changing this
        spawnPoints = new GameObject[4];
    }

    // Start is called before the first frame update
    void Start()
    {
        if (AltNumberOfLaps>0) {
            NumberOfLaps = AltNumberOfLaps;
        }
        Setup();
    }

    public void FinishRace() {
        if (NextLevel == null || NextLevel == "" || SceneManager.GetSceneByName(NextLevel).IsValid()) {
            SceneManager.LoadScene("MainMenu");
        } else {
            SceneManager.LoadScene(NextLevel);
        }
    }

    private void Setup() {
        //error out if there is no starting grid.
        if (spawnPoints == null || spawnPoints.Length == 0) {
            throw new System.Exception("There is no starting grid. Please place the StartingGrid prefab in an appropriate location in the level and try again.");
        }

        //or if there are no players
        if (PlayerInfo.NumberOfPlayers<1) {
            throw new System.Exception("There are no players, or the NumberOfPlayers variable was not set.");
        }

        //or if there are no cars.
        if (PlayerInfo.PlayerCars == null || PlayerInfo.PlayerCars.Length == 0) {
            throw new System.Exception("There are no cars in the PlayerCars array, or it doesn't exist.");
        }

        for (int i = 0; i < PlayerInfo.NumberOfPlayers; i++) {
            Instantiate(PlayerInfo.PlayerCars[i].gameObject, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
        }
    }
}
