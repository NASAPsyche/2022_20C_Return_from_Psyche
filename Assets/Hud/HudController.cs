using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    public Text sampleNum;
    private String sceneName;

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
            if(sceneName == "Level1")
                sampleNum.text = "0/4";
            if(sceneName == "Level2")
                sampleNum.text = "1/4";
            if(sceneName == "Level3")
                sampleNum.text = "2/4";
            if(sceneName == "Level4")
                sampleNum.text = "3/4";
            if(sceneName == "Level5")
                sampleNum.text = "4/4";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
