using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
    [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .25f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
    [SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching
    [SerializeField] private float slipVel;
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    private bool canDoubleJump = false; // Whether or not the player can double jump
    private bool hasJetpack = false;    // Whether or not the player has collected the jetpack
    private bool hasClimbingGear = false; //Whether or not the player has collected the climbing gear 
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private string sceneName; 

    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool m_wasCrouching = false;

    private bool canClimb = false;
    const float m_ClimbSpeed = 10f; // Player climbing speed
    private float defaultGravity;   

    public HudController hudControl; //Used to call fade to black transitions

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();

        if (OnCrouchEvent == null)
            OnCrouchEvent = new BoolEvent();

        defaultGravity = m_Rigidbody2D.gravityScale;

        //make jetpack item and climbing gear item carry over scenes
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level3" || sceneName == "Level4" || sceneName == "Level5")
        {
            hasJetpack = true;
        }
        if (sceneName == "Level5")
        {
            hasClimbingGear = true;
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //upon colliding with an object, check if the object was one of the gimmick items. If it is, give the player the gimmick item
        if(collision.collider.name == "jetpack")
        {
            hasJetpack = true;
        }
        if (collision.collider.name == "backpack")
        {
            hasClimbingGear = true;
        }
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.collider.name == "Oil_Spill" && Mathf.Abs(m_Rigidbody2D.velocity.x) >= 0.1)
        {
            m_Rigidbody2D.AddForce(new Vector2(slipVel*Mathf.Sign(m_Rigidbody2D.velocity.x), 0f));
        }

        // Upon colliding with a climbing wall object and having picked up climbing gear, the player s able to climb
        if(other.collider.name == "Climbing_Wall" && hasClimbingGear == true)
        {
            canClimb = true;
        }
        else
        {
            canClimb = false;
        }    

        // Upon colliding with the Sample object and pressing the 'e' key, the player will transition to the next scene
        if((other.collider.name == "Scene_Transition") && (Input.GetKey(KeyCode.E)))
        {
            sceneName = SceneManager.GetActiveScene().name;
            if(sceneName == "Level1")
            {
                hudControl.FadeInOrOut();
                SceneManager.LoadScene("Level2");
            }
            if(sceneName == "Level2")
            {
                hudControl.FadeInOrOut();
                SceneManager.LoadScene("Level3");
            }
            if(sceneName == "Level3")
            {
                hudControl.FadeInOrOut();
                SceneManager.LoadScene("Level4");
            }
            if(sceneName == "Level4")
            {
                hudControl.FadeInOrOut();
                SceneManager.LoadScene("Level5");
            }
            if(sceneName == "Level5")
            {
                hudControl.FadeInOrOut();
                SceneManager.LoadScene("Closing Page");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.collider.name == "Climbing_Wall")
        {
            canClimb = false;
        }
    }


    public bool getGround()
    {
        return m_Grounded;
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {

            // If crouching
            if (crouch)
            {
                if (!m_wasCrouching)
                {
                    m_wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                // Reduce the speed by the crouchSpeed multiplier
                move *= m_CrouchSpeed;

                // Disable one of the colliders when crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = false;
            }
            else
            {
                // Enable the collider when not crouching
                if (m_CrouchDisableCollider != null)
                    m_CrouchDisableCollider.enabled = true;

                if (m_wasCrouching)
                {
                    m_wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }
            }

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
        }
        // If the player should jump...
        if (jump)
        {
            if (m_Grounded)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                canDoubleJump = true; //allow the player to double jump while still in the air
            }
            else if(canDoubleJump && hasJetpack)
            {
                m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
                // Add a vertical force to the player.
                m_Grounded = false;
                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                canDoubleJump = false; //do not allow the player to jump again after double jumping
            }
        }

        // Allows the player to climb when moving towards and colliding with a climbing wall
        if(canClimb && (move != 0))
        {
            // Sets gravity to 0 and creates a new constant velocity
            m_Rigidbody2D.gravityScale = 0f;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_ClimbSpeed);
        }
        else
        {
            // Resets gravity to defualt
            m_Rigidbody2D.gravityScale = defaultGravity;
        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}