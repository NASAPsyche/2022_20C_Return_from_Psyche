using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    GameObject player;
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        initialPosition = player.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y <= -50)
        {
            player.transform.position = initialPosition;
        }
    }
}
