using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("here");
        if(other.tag == "pickup")
        {
            door.transform.Translate(-10,0,0);
            Debug.Log("Placed");
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("here");
        if(other.tag == "pickup")
        {
            door.transform.Translate(10,0,0);
            Debug.Log("Placed");
        }
    }
}
