using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;


    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump;
    float verticalMove = 0f;
    
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical");
        if (verticalMove > 0)
            jump = true;
        else
            jump = false; 
        
    }

    void FixedUpdate()
    {
        //Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        
    }





}
