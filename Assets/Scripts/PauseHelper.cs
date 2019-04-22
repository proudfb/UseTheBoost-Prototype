using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class for pausing
/// </summary>
public class PauseHelper : MonoBehaviour
{
    private static bool ispaused = false;
    public static bool IsPaused { get; }

    void Start()
    {
        ////always start unpaused
        //ispaused = false;
        //Time.timeScale = 1f;
        //Time.fixedDeltaTime = 1f;
    }

    public static void Pause() {
        ispaused = true;
        Time.timeScale = 0f;
        //Time.fixedDeltaTime = 0f;
    }

    public static void Unpause() {
        ispaused = false;
        Time.timeScale = 1f;
        //Time.fixedDeltaTime = 1f;
    }

}
