using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    private Animator anim;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump;
    float verticalMove = 0f;

    void Awake()
    {
        //grab references for animator from game object
        anim = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical");
        if (verticalMove > 0)
            jump = true;
        else
            jump = false;



        //animation parameters
        anim.SetBool("run", controller.getGround() == true && horizontalMove != 0);
        anim.SetBool("jumpAnimation", controller.getGround() == false);
        anim.SetBool("ground", controller.getGround() == true);
    }

    void FixedUpdate()
    {
        //Move the character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        
    }





}
