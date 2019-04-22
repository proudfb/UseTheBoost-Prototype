using Statics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Vehicles.Car;

/// <summary>
/// Play a countdown with associated graphics, and ensure players can't move during it. (also add a cheeky boost)
/// </summary>
[RequireComponent(typeof(Canvas))]
public class Countdown : MonoBehaviour
{ 
    //Includes 3 audio clips: Ready, Countdown (3, 2, 1), and Go
    public AudioClip[] countdownClips = new AudioClip[5];
    private Text countdownText;
    private AudioSource au;
    private CarUserControl[] players;

    void Start()
    {
        if (gameObject.GetComponent<AudioSource>() == null) {
            au = gameObject.AddComponent<AudioSource>() as AudioSource;
        }

        countdownText = gameObject.GetComponentInChildren<Text>();

        //Tell everyone that we're in a countdown state.
        players = FindObjectsOfType<CarUserControl>();
        foreach (CarUserControl player in players) {
            player.IsCountdown = true;
        }
        PlayerInfo.IsCountDown = true;
        StartCoroutine(PlayCountdownAsync());
    }

    private IEnumerator PlayCountdownAsync() {
        countdownText.text = "Ready";
        au.PlayOneShot(countdownClips[0]);
        while (au.isPlaying) {
            //wait for Ready to finish
            yield return null;
        }
        yield return new WaitForSeconds(1);
        //play the countdown
        countdownText.text = "3";
        au.PlayOneShot(countdownClips[1]);
        while (au.isPlaying) {
            //wait for Ready to finish
            yield return null;
        }
        countdownText.text = "2";
        au.PlayOneShot(countdownClips[2]);
        while (au.isPlaying) {
            //wait for Ready to finish
            yield return null;
        }
        countdownText.text = "1";
        au.PlayOneShot(countdownClips[3]);
        while (au.isPlaying) {
            //wait for Ready to finish
            yield return null;
        }
        //get everyone out of the countdown state
        PlayerInfo.IsCountDown = false;
        foreach (CarUserControl player in players) {
            player.IsCountdown = false;
            //cheeky boost like in F-Zero
            //player.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 50, ForceMode.VelocityChange);
        }
        //GO
        countdownText.text = "GO";
        au.PlayOneShot(countdownClips[4]);
        while (au.isPlaying) {
            //wait for Ready to finish
            yield return null;
        }
        countdownText.text = "";
        Destroy(this);
        yield return null;
    }

}
