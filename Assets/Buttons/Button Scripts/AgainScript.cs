using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgainScript : MonoBehaviour
{
    public void OnButtonPress()
    {
        SceneManager.LoadScene("Level1");
    }
}
