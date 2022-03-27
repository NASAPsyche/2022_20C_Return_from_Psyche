using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HudController : MonoBehaviour
{
    public GameObject blackOutSquare;
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

        int fadespeed = 1;
        StartCoroutine(FadeBlackOutSquare(false, fadespeed));
    }

    public void FadeInOrOut(bool fadeToBlack = true)
    {
        StartCoroutine(FadeBlackOutSquare(fadeToBlack));
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if(fadeToBlack)
        {
            while(blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (((float)fadeSpeed/2) * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while(blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
    }
}

