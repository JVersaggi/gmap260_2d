using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class WebSystem : MonoBehaviour
{
    public GameObject webHingeAnchor;
    public DistanceJoint2D webJoint;
    public Transform crosshair;
    public SpriteRenderer crosshairSprite;
    private bool webAttached;
    private Vector2 playerPosition;
    private Rigidbody2D webHingeAnchorRb;
    private SpriteRenderer webHingeAnchorSprite;

    public LineRenderer webRenderer;
    public LayerMask webLayerMask;
    private float webMaxCastDistance = 7f;
    private List<Vector2> webPositions = new List<Vector2>();

    private player_move_prot movement; 

    private bool distanceSet;


    void Awake()
    {
        // 2
        crosshairSprite.enabled = false;
        webJoint.enabled = false;
        playerPosition = transform.position;
        webHingeAnchorRb = webHingeAnchor.GetComponent<Rigidbody2D>();
        webHingeAnchorSprite = webHingeAnchor.GetComponent<SpriteRenderer>();
        movement = GetComponent<player_move_prot>();
    }

    void Update()
    {

        Vector2 aimDirection = new Vector2(0,0);
        if (GetComponent<SpriteRenderer>().flipX)
        {
            aimDirection = Quaternion.Euler(0, 0, 300f) * Vector2.left;
        }
        else
        {
            aimDirection = Quaternion.Euler(0, 0, 60f) * Vector2.right;
        }
        

        playerPosition = transform.position;


        if (!webAttached)
        {

            if (GetComponent<SpriteRenderer>().flipX)
            {
                SetCrosshairPosition(90f);
            }
            else
            {
                SetCrosshairPosition(45f);
            }
        }
        else
        {
            crosshairSprite.enabled = false;
        }
        HandleInput(aimDirection);

        UpdatewebPositions();
    }

    private void SetCrosshairPosition(float aimAngle)
    {
       
        
        var x = transform.position.x + 1f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 1f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crosshair.transform.position = crossHairPosition;
    }
    
    private void HandleInput(Vector2 aimDirection)
    {
        if (Input.GetButtonDown("Fire1") && !webAttached && !movement.isGrounded)
        {
            
            webRenderer.enabled = true;

            var hit = Physics2D.Raycast(playerPosition, aimDirection, webMaxCastDistance, webLayerMask);

        
            if (hit.collider != null)
            {
                webAttached = true;
                if (!webPositions.Contains(hit.point))
                {
                    webPositions.Add(hit.point);
                    webJoint.distance = Vector2.Distance(playerPosition, hit.point);
                    webJoint.enabled = true;
                    webHingeAnchorSprite.enabled = true;
                    //Debug.Log(webJoint.distance);
                }
            }
       
            else
            {
                webRenderer.enabled = false;
                webAttached = false;
                webJoint.enabled = false;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Resetweb();
        }
    }
    
    private void Resetweb()
    {
        webJoint.enabled = false;
        webAttached = false;
        webRenderer.positionCount = 2;
        webRenderer.SetPosition(0, transform.position);
        webRenderer.SetPosition(1, transform.position);
        webPositions.Clear();
        webHingeAnchorSprite.enabled = false;
    }

    private void UpdatewebPositions()
    {
        if (!webAttached)
        {
            return;
        }
        webRenderer.positionCount = webPositions.Count + 1;       
        Debug.Log("HELLO");
        for (var i = webRenderer.positionCount - 1; i >= 0; i--)
        {
            if (i != webRenderer.positionCount - 1)
            {
                webRenderer.SetPosition(i, webPositions[i]);
                
                if (i == webPositions.Count - 1 || webPositions.Count == 1)
                {
                    var webPosition = webPositions[webPositions.Count - 1];
                    if (webPositions.Count == 1)
                    {
                        webHingeAnchorRb.transform.position = webPosition;
                        if (!distanceSet)
                        {
                            webJoint.distance = Vector2.Distance(transform.position, webPosition);
                            distanceSet = true;
                        }
                    }
                    else
                    {
                        webHingeAnchorRb.transform.position = webPosition;
                        if (!distanceSet)
                        {
                            webJoint.distance = Vector2.Distance(transform.position, webPosition);
                            distanceSet = true;
                        }
                    }
                }
                else if (i - 1 == webPositions.IndexOf(webPositions.Last()))
                {
                    var webPosition = webPositions.Last();
                    webHingeAnchorRb.transform.position = webPosition;
                    if (!distanceSet)
                    {
                        webJoint.distance = Vector2.Distance(transform.position, webPosition);
                        distanceSet = true;
                    }
                }
            }
            else
            {
                webRenderer.SetPosition(i, transform.position);
            }
        }
    }

}
