using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public float moveSpeed;
    public float jumpForce;
    public bool sliding = false;
    public bool grounded;
    public LayerMask whatIsGround;
    private Collider2D myCollider;
    private Animator myAnimator;
    Vector2 fingerStart;
    Vector2 fingerEnd;
    public int playerSpeed = 10;
    private bool facingRight = false;
    public int playerJumpPower = 1250;
    private float moveX;
    public bool isGrounded;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
        myAnimator = GetComponent<Animator>();

    }
    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerRaycast();

        if ((Input.GetButtonDown("Vertical") && !sliding))
        {
            myAnimator.SetBool("isSliding", true);
            sliding = true;
        }

        grounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        if (grounded)
        {
            myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerStart = touch.position;
                    fingerEnd = touch.position;
                }

                if (touch.phase == TouchPhase.Moved)
                {
                    fingerEnd = touch.position;
                    if (Mathf.Abs(fingerEnd.y - fingerStart.y) > 50)//Vertical swipe
                    {
                        if (fingerEnd.y - fingerStart.y > 50)//up swipe
                        {
                            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                        }
                    }
                }
            }
        }
        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("grounded", grounded);
    }
    void PlayerMove()
    {
        myAnimator.SetBool("Run_Anim", true);
        sliding = true;
        //controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
        //animation
        //player direction
        if (moveX < 0.0f && facingRight == false)
        {
            FlipPlayer();
        }
        else if (moveX > 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        //physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //Jumping Code
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        isGrounded = false;
    }

    //change direction
    void FlipPlayer()
    {
        facingRight = !facingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    //Checks if the player is on the ground
    /*private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Player has collided with " + col.collider.name);
        if (col.gameObject.tag == "ground")
        {
            isGrounded = true;
        }
    }*/

    void PlayerRaycast ()
    {
        RaycastHit2D hit = Physics2D.Raycast (transform.position, Vector2.down);
        //bounce off enemies
        if (hit != null && hit.collider != null && hit.distance < 0.9f && hit.collider.tag == "Enemy")
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 1000);
        }
        //jump off of other blocks
        if (hit != null && hit.collider != null && hit.distance < 0.9f && hit.collider.tag != "Enemy")
        {
            isGrounded = true;
        }
    }
}