using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class psycheBGM : MonoBehaviour
{
    private void Awake()
    {
        int numBGM = FindObjectsOfType<psycheBGM>().Length;
        if (numBGM != 1)
        {
            Destroy(this.gameObject);
        }
            //more than one BGM
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}