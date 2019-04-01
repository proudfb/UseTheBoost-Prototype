using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;

public static class PlayerInfo {
    public static int NumberOfPlayers { get; set; } = 0;
    public static CarController[] PlayerCars { get; set; } = new CarController[4];

    public static void ResetCars() {
        if (NumberOfPlayers<1) {
            PlayerCars = new CarController[1];
        }
        else PlayerCars = new CarController[NumberOfPlayers];
    }
}
    
