using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Exit : MonoBehaviour
{    public void OnButtonPress()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title Screen");
    }
}