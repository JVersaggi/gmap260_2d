using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private GameObject child;
    private GameObject pick_up;
    private bool in_range = false;
    public GameObject player;
    private bool picked_up = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D ray_forward;
        if (GetComponent<SpriteRenderer>().flipX)
        {
            ray_forward = Physics2D.Raycast(transform.position, Vector2.left);
        }
        else
        {
            ray_forward = Physics2D.Raycast(transform.position, Vector2.right);
        }
        Debug.Log(ray_forward.distance);
        if (!picked_up)
        {
            if (child != null && (ray_forward == false || ray_forward.distance >= 1 || ray_forward.rigidbody.gameObject.transform.GetChild(0).gameObject != child))
            {
                child.GetComponent<SpriteRenderer>().enabled = false;
                child = null;
                in_range = false;
                pick_up = null;
            }
            else if (ray_forward && ray_forward.distance < 1)
            {
                pick_up = ray_forward.rigidbody.gameObject;
                child = ray_forward.rigidbody.gameObject.transform.GetChild(0).gameObject;
                child.GetComponent<SpriteRenderer>().enabled = true;
                in_range = true;
            }
        }

        if (Input.GetButtonDown("Fire2") && in_range)
        {
            Debug.Log("YEEt");
            child.GetComponent<SpriteRenderer>().enabled = false;
            child = null;
            in_range = false;
            pick_up.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; 
            pick_up.transform.parent = player.transform;
            pick_up.transform.position = new Vector2(transform.position.x, transform.position.y + 1f);
            pick_up.GetComponent<BoxCollider2D>().enabled = false;
            picked_up = true;
        }

        if (Input.GetButtonUp("Fire2") && picked_up)
        {
            pick_up.transform.parent = null;
            pick_up.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            pick_up.transform.position = new Vector2(transform.position.x + 1f, transform.position.y);
            pick_up.GetComponent<BoxCollider2D>().enabled = true;
            child = null;
            in_range = false;
            pick_up = null;
            picked_up = false;
        }
    }
}
