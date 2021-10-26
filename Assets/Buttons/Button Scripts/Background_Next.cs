using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Background_Next : MonoBehaviour
{    public void OnButtonPress()
    {
        SceneManager.LoadScene("Start Page");
    }
}