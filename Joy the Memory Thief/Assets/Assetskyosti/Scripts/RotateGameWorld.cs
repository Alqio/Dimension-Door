using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGameWorld : MonoBehaviour {

    private GameObject[] platforms;

    public float rotationSpeed = 1f;
    public float zoomedOut     = 110f;
    public float zoomedIn      = 8f;

    public Sprite normalSprite;
    public Sprite selectedSprite;

    private PlayerControl controlScript;

    private Transform playerTransform;
    public int ringNumber = 0;
    private int maxRing = 2;
    public int targetAngle = 0;

    private bool rotating;
    private Vector3 rotateDirection;
    private float rotated = 0;

    private void Awake()
    {
        playerTransform = GetComponent<Transform>();
        controlScript = GetComponent<PlayerControl>();
    }

    // Use this for initialization
    void Start () {
        rotating = false;
    }
	
    public void Rotate(Vector3 direction)
    {
        platforms = GameObject.FindGameObjectsWithTag("Ring" + ringNumber);
        
        foreach (GameObject platform in platforms)
        {
            foreach (Transform child in platform.transform)
            {
                Quaternion rot = child.transform.rotation;

                child.transform.RotateAround(Vector3.zero, direction, rotationSpeed);

                if (child.CompareTag("Coin"))
                {
                    child.transform.rotation = rot;
                }
            }
        }
    }

    public void HandleInput(Rigidbody2D body)
    {
        if (Input.GetKeyDown(KeyCode.Q) && body.position == Vector2.zero)
        {
            //Rotate(new Vector3(0, 0, -1));
            rotating = true;
            rotateDirection = new Vector3(0, 0, -1);
        }

        if (Input.GetKeyDown(KeyCode.E) && body.position == Vector2.zero)
        {
            //Rotate(new Vector3(0, 0, 1));
            rotating = true;
            rotateDirection = new Vector3(0, 0, 1);
        }
        if (rotating)
        {
            Rotate(rotateDirection);

            rotated += rotationSpeed;
            
            if (rotated >= 90)
            {
                rotating = false;
                rotated = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(body.position.x) < 10.5f && Mathf.Abs(body.position.y) < 10.5f)
        {
            controlScript.toCenter = !controlScript.toCenter;
            if (controlScript.toCenter)
            {
                body.velocity = new Vector2((float)-body.position.x, (float)-body.position.y);
                body.gravityScale = 0;
                controlScript.targetZoom = zoomedOut;
                ColorSelectedPlatforms();
            }
            else
            {
                body.gravityScale = 1;
                controlScript.targetZoom = zoomedIn;
                ResetPlatformColors();
            }
            //controlScript.ResetZoomSpeed();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && body.position == Vector2.zero && !rotating)
        {
            DecreaseRing();
            ColorSelectedPlatforms();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && body.position == Vector2.zero && !rotating)
        {
            IncreaseRing();
            ColorSelectedPlatforms();
        }
    }

    public void ResetPlatformColors()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject[] platforms1 = GameObject.FindGameObjectsWithTag("Ring" + i);
            foreach (GameObject platform in platforms1)
            {

                foreach (Transform child in platform.transform)
                {
                    if (!child.CompareTag("Coin"))
                    {
                        child.GetComponent<SpriteRenderer>().sprite = normalSprite;
                    }
                }

            }
            
        }
    }

    public void ColorSelectedPlatforms()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject[] platforms1 = GameObject.FindGameObjectsWithTag("Ring" + i);
            foreach (GameObject platform in platforms1)
            {
                foreach(Transform child in platform.transform)
                {
                    if(!child.CompareTag("Coin"))
                    {
                        child.GetComponent<SpriteRenderer>().sprite = normalSprite;
                    }
                }
                /*
                if (platform.GetComponent<SpriteRenderer>() != null)
                {
                    platform.GetComponent<SpriteRenderer>().sprite = normalSprite;
                }
                 */
                
            }
        }
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Ring" + ringNumber);
        foreach (GameObject platform in platforms)
        {
            foreach (Transform child in platform.transform)
            {
                if (!child.CompareTag("Coin"))
                {
                    child.GetComponent<SpriteRenderer>().sprite = selectedSprite;
                }
            }
            /*
            if (platform.GetComponent<SpriteRenderer>() != null)
            {
                platform.GetComponent<SpriteRenderer>().sprite = selectedSprite;
            }
             */
        }
    }

    public void IncreaseRing()
    {
        ringNumber = Mathf.Min(ringNumber + 1, maxRing);
    }

    public void DecreaseRing()
    {
        ringNumber = Mathf.Max(ringNumber - 1, 0);
    }

}
