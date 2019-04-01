using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeButton : MonoBehaviour
{
    public UnityStandardAssets.Vehicles.Car.CarController playerCar;

    public void LoadByName(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadBySceneNumber(int sceneNumber) {
        PlayerInfo.NumberOfPlayers = 1;
        PlayerInfo.PlayerCars[0] = playerCar;
        SceneManager.LoadScene(sceneNumber);
    }
}
