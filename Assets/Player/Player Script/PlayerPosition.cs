using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPosition : MonoBehaviour
{
    GameObject player;
    GameObject checkpoint;
    Vector3 initialPosition;
    bool checkPointReached = false;
    float playerPositionX;
    float playerPositionY;
    float checkpointPositionX;
    float checkpointPositionY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        
        //look for checkpoint if current level is level5
        if(SceneManager.GetActiveScene().name == "Level5" || SceneManager.GetActiveScene().name == "Level3")
        {
            checkpoint = GameObject.Find("Checkpoint");
            checkpointPositionX = checkpoint.transform.position.x;
            checkpointPositionY = checkpoint.transform.position.y;
        }       

        //load initial position
        initialPosition = player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((SceneManager.GetActiveScene().name == "Level5" || SceneManager.GetActiveScene().name == "Level3") && checkPointReached == false)
        {
            //check if player has reached checkpoint
            playerPositionX = player.transform.position.x;
            playerPositionY = player.transform.position.y;
            if (System.Math.Abs(playerPositionX - checkpointPositionX) < 10 && System.Math.Abs(playerPositionY - checkpointPositionY) < 50)
            {
                checkPointReached = true;
            }
        }

        //check if player has fallen off edge
        if (player.transform.position.y <= -50)
        {
            if(checkPointReached == false)
            {
                player.transform.position = initialPosition;
            }
            else
            {
                player.transform.position = checkpoint.transform.position;
            }      
        }
    }
}
