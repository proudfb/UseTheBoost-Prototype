using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScores : MonoBehaviour
{
    public Text times;
    const string TIMEFORMAT = "Lap {0}: {1:mm\\:ss\\.fff}\n";
    //private TimeSpan lap1, lap2, lap3, total;

    // Start is called before the first frame update
    void Start()
    {
        times = gameObject.GetComponent<Text>();
    }

    public void DisplayTimes(float[] timestamps) {
        TimeSpan span = TimeSpan.FromSeconds(0d);
        StringBuilder sb = new StringBuilder("");
        for (int i = 1; i < timestamps.Length; i++) {
            span = TimeSpan.FromSeconds(timestamps[i] - timestamps[i - 1]);
            sb.AppendFormat(TIMEFORMAT, i, span);
        }
        //get the difference between start and end times.
        span = TimeSpan.FromSeconds(timestamps[timestamps.Length - 1] - timestamps[0]);
        sb.AppendFormat("Total: {0:mm\\:ss\\.fff}", span);
        times.text = sb.ToString();
    }
    
}
