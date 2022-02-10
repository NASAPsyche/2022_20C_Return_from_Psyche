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

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "Level1")
        {
            currentTime = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(stopwatchActive == true)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        currentTimetext.text = time.ToString(@"mm\:ss\:fff");
    }

    public void StartStopwatch()
    {
        stopwatchActive = true;
    }

    public void StopStopwatch()
    {
        stopwatchActive = false;
    }
}
