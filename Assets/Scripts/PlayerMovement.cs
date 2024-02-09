using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float climbSpeed = 8f;
    [SerializeField] float runSpeed = 6f;
    [SerializeField] float jumpHeight = 20f;
    [SerializeField] Vector2 deathFlop = new Vector2(10f, 10f);
    [SerializeField] AudioClip deathSound;

    float gravityAtStart;
    bool isAlive = true;
    

    Vector2 moveInput;
    Rigidbody2D myR2D;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    // Start is called before the first frame update
    void Start()
    {
        //Component References
        myR2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityAtStart = myR2D.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        FlipSprite();
        Run();
        Climb();
        Die();
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        //Log movement
        //Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }
        

        if (value.isPressed)
        {
            myR2D.velocity += new Vector2(0f, jumpHeight);
        }
    }



    void Run()
    {
        //Horiztonal reference to the user input for the x axis
        //Keep currrent Vertical velocity the same
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myR2D.velocity.y);
        //Use the reference and set it to a new point on the x axis
        myR2D.velocity = playerVelocity;

        bool playerHorizontalMovement = Mathf.Abs(myR2D.velocity.x) > Mathf.Epsilon;

        myAnimator.SetBool("isRunning", playerHorizontalMovement);

    }

    void FlipSprite()
    {
        //aboslute value of movement is greater than 0
        bool playerHorizontalMovement = Mathf.Abs(myR2D.velocity.x) > Mathf.Epsilon;

        //if there is movement
        if (playerHorizontalMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(myR2D.velocity.x), 1f);
        }

    }


    void Climb()
    {
        
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climb")))
        {
            myR2D.gravityScale = gravityAtStart;
            return;
        }
        
        //Apply climb velociity to y axis
        Vector2 climbDistance = new Vector2(myR2D.velocity.x, moveInput.y * climbSpeed);
        myR2D.velocity = climbDistance;
        myR2D.gravityScale = 0f;

    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Traps", "Enemies")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myR2D.velocity = deathFlop;
            //Audio soound
            AudioSource.PlayClipAtPoint(deathSound, this.transform.position, 2);
            FindObjectOfType<GameManager>().ProcessPLayerDeath();
        }
    }
}
