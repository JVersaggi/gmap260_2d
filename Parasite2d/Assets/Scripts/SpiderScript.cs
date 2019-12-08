using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiderScript : MonoBehaviour
{
    private LineRenderer line;
    public GameObject anchor;
    public string level;
    // Start is called before the first frame update
    void Start()
    {
        line = this.GetComponent<LineRenderer>();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        line.SetPosition(0,anchor.transform.position);
        line.SetPosition(1,hit.point);
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0,anchor.transform.position);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadScene(level);
        }
    }
}
