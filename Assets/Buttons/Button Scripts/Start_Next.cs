using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_Next : MonoBehaviour
{    public void OnButtonPress()
    {
        SceneManager.LoadScene("Title Screen");
    }
}