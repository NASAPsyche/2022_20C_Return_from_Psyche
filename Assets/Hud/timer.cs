using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class timer : MonoBehaviour
{
    static bool stopwatchActive = true;
    static float currentTime;
    public Text currentTimetext;
    private String sceneName;

    // Sets stopwatch to 0 when starting level 1 and stops the stopwatch when starting the closing page
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Level1")
        {
            StartStopwatch();
            currentTime = 0;
        }
        else if(sceneName == "Closing Page")
        {
            StopStopwatch();
        }
    }

    // Updates and displays the stopwatch
    void Update()
    {
        if(stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimetext.text = time.ToString(@"mm\:ss\:ff");
    }

    // Starts the stopwatch
    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    // Stops the stopwatch
    public void StopStopwatch()
    {
        stopwatchActive = false;
    }
}
