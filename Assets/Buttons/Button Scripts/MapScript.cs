using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapScript : MonoBehaviour
{
    public void OnButtonPress()
    {
        PlayerPrefs.SetInt("lastscene", SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Map");
    }
   
}
