using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ANT : MonoBehaviour
{

    public string level;
    int modifier = -1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveFlip());
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(modifier * .5f, 0);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(level);
        }
    }

    IEnumerator MoveFlip()
    {
        yield return new WaitForSeconds(3);
        modifier = modifier * -1;
        this.GetComponent<SpriteRenderer>().flipX = !this.GetComponent<SpriteRenderer>().flipX;
        StartCoroutine(MoveFlip());
    }
}
