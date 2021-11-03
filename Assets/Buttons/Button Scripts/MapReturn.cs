using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MapReturn : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("lastscene")); 
    }
}
