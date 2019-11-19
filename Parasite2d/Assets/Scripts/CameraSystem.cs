using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private GameObject player;
    public float xMin = -1.6f;
    public float xMax = 24f;
    public float yMin = 1.2f;
    public float yMax = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float x = Mathf.Clamp (player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp (player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);

    }
}
