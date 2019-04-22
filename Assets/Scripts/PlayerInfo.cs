using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;


namespace Statics
{
    public static class PlayerInfo
    {
        /// <summary>
        /// The number of players in the game
        /// </summary>
        public static int NumberOfPlayers { get; set; } = 0;
        /// <summary>
        /// The cars for each player. Max of 4
        /// </summary>

        public static CarController[] PlayerCars { get; set; } = new CarController[4];

        //Are we in the countdown?
        public static bool IsCountDown { get; set; }

        public static void ResetCars() {
            if (NumberOfPlayers < 1) {
                PlayerCars = new CarController[1];
            } else PlayerCars = new CarController[NumberOfPlayers];
        }


    } 
}
    
