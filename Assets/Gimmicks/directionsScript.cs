using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionsScript : MonoBehaviour
{
    //the x and y positions of the player will be used to check how close the player is to the directions
    float playerPositionX;
    float playerPositionY;

    //the x and y positions of the directions will be used to check how close the player is to the directions
    float directionsPositionX;
    float directionsPositionY;

    // Update is called once per frame
    void Update()
    {
        //get x and y positions for player and directions
        playerPositionX = GameObject.Find("Player").transform.position.x;
        directionsPositionX = transform.position.x;
        playerPositionY = GameObject.Find("Player").transform.position.y;
        directionsPositionY = transform.position.y;

        //check if playerPosition is close enough to text box to display the textbox
        if (System.Math.Abs(playerPositionX - directionsPositionX) < 10 && System.Math.Abs(playerPositionY - directionsPositionY) < 10)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
        }
            
    }
}
