using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_move_prot : MonoBehaviour
{

    public int playerSpeed = 10;
    public int playerJumpPower = 1250;
    private float moveX;
    public bool isGrounded;
    public float distanceToBottomofPlayer = 1.3f;

    // Update is called once per frame
    void Update()
    {
        PlayerMove ();
        PlayerRaycast();
    }

    void PlayerMove()
    {
        //controls
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }

        //animation
        if (moveX != 0)
        {
            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
        }

        //player direction
        if (moveX < 0.0f )
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (moveX > 0.0f) {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //physics
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //Jumping Code
        //GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        Vector2 PlayerVelo = GetComponent<Rigidbody2D>().velocity;
        PlayerVelo.y = 5;
        GetComponent<Rigidbody2D>().velocity = PlayerVelo;
        isGrounded = false;
        //Debug.Log(isGrounded);
    }

    void PlayerRaycast ()
    {
        RaycastHit2D rayUP = Physics2D.Raycast(transform.position, Vector2.up);
        if (rayUP != null && rayUP.collider != null && rayUP.distance < distanceToBottomofPlayer && rayUP.collider.name == "Box2")
        {
            Destroy(rayUP.collider.gameObject);
        }

        RaycastHit2D rayDown = Physics2D.Raycast (transform.position, Vector2.down);
        
        //jump off of other blocks
        //Debug.Log(rayDown.distance);
        if (rayDown != null && rayDown.collider != null && rayDown.distance < distanceToBottomofPlayer && rayDown.collider.tag != "Enemy")
        {
            isGrounded = true;
        }
    }
}
