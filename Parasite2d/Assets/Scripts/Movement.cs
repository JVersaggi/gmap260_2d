using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    public int maxjumps = 2;
    int jumps;
    float jumpforce = 5f;
    bool grounded;
    mbox     
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }
    void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.tag == "Ground")
        {
            jumps = maxjumps;
            grounded = true;
            movespeed = 2.0F;
        }
    }
    void Jump()
    {
        if (jumps > 0)
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
            grounded = false;
            jumps = jumps - 1;
        }
        if (jumps == 0)
        {
            return;
        }
    }

}