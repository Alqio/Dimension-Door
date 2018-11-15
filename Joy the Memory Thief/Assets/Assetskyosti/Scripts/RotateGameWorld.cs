using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RotateGameWorld : MonoBehaviour {
    
    private List<GameObject> activePlatforms = new List<GameObject>();
    public GameObject activeMaze;
    
    public float rotationSpeed;
    public float zoomedOut;
    public float zoomedIn;

    public Sprite normalSprite;
    public Sprite selectedSprite;

    private PlayerControl controlScript;

    private Transform playerTransform;
    public int ringNumber = 0;
    private int maxRing = 3;
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
        rotationSpeed = 3f;
        zoomedOut = 110f;
        zoomedIn = 8f;
        rotating = false;
    }
	
    public void Rotate(Vector3 direction)
    {
        foreach(GameObject platform in activePlatforms)
        {
            platform.transform.RotateAround(activeMaze.transform.position, direction, rotationSpeed);
        }
    }

    public bool BodyInCenter(Rigidbody2D body)
    {
        if(activeMaze != null)
        {
            return body.position == (Vector2)activeMaze.transform.position;
        } else
        {
            return false;
        }
    }

    public void HandleInput(Rigidbody2D body)
    {
        if (Input.GetKeyDown(KeyCode.Q) && BodyInCenter(body))
        {
            rotating = true;
            rotateDirection = new Vector3(0, 0, -1);
        }

        if (Input.GetKeyDown(KeyCode.E) && BodyInCenter(body))
        {
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
        if(controlScript.toCenter)
        {
            MoveTowardsCenter(body);
        }

        if (Input.GetKeyDown(KeyCode.W) && activeMaze != null)
        {
            controlScript.toCenter = !controlScript.toCenter;
            if (controlScript.toCenter)
            {
                body.velocity = (Vector2)activeMaze.transform.position - body.position;
                body.gravityScale = 0;
                controlScript.targetZoom = zoomedOut;
                SetActivePlatforms();
                ColorSelectedPlatforms();
            }
            else
            {
                body.gravityScale = 1;
                controlScript.targetZoom = zoomedIn;
                ResetPlatformColors();
            }
            controlScript.ResetZoomSpeed();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && BodyInCenter(body) && !rotating)
        {
            DecreaseRing();
            ResetPlatformColors();
            SetActivePlatforms();
            ColorSelectedPlatforms();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && BodyInCenter(body) && !rotating)
        {
            IncreaseRing();
            ResetPlatformColors();
            SetActivePlatforms();
            ColorSelectedPlatforms();
        }
    }

    public void SetActivePlatforms()
    {
        if(activeMaze != null)
        {
            if (activeMaze.tag == "center" && ringNumber == maxRing)
            {
                Transform mazes = GameObject.FindGameObjectWithTag("Mazes").transform;
                activePlatforms.Clear();
                foreach (Transform maze in mazes)
                {
                    if(maze.gameObject.tag != "center")
                    {
                        activePlatforms.Add(maze.gameObject);
                        /*
                        Transform blocks = maze.transform.Find("Offset").Find("Blocks");
                        foreach (Transform ring in blocks)
                        {
                            foreach(Transform platform in ring)
                            {
                                activePlatforms.Add(platform.gameObject);
                            }
                        }
                         */
                    }
                }
            }
            else
            {
                //Transform ring = activeMaze.transform.Find("Offset").Find("Blocks").Find("Ring" + ringNumber);
                activePlatforms.Clear();
                activePlatforms.Add(activeMaze.transform.Find("Offset").Find("Blocks").Find("Ring" + ringNumber).gameObject);
                /*
                foreach (Transform child in ring)
                {
                    activePlatforms.Add(child.gameObject);
                }
                 */
            }
        }
    }

    public void ResetPlatformColors()
    {
        foreach (GameObject maze in activePlatforms)
        {
            List<GameObject> platforms = new List<GameObject>();
            AddDescendantsWithTag(maze.transform, "Block", platforms);
            foreach (GameObject platform in platforms)
            {
                platform.GetComponent<SpriteRenderer>().sprite = normalSprite;
            }
        }
    }

    public void ColorSelectedPlatforms()
    {
        foreach (GameObject maze in activePlatforms)
        {
            List<GameObject> platforms = new List<GameObject>();
            AddDescendantsWithTag(maze.transform, "Block", platforms);
            foreach (GameObject platform in platforms)
            {
                platform.GetComponent<SpriteRenderer>().sprite = selectedSprite;
            }
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

    void MoveTowardsCenter(Rigidbody2D body)
    {
        if (controlScript.toCenter && activeMaze != null)
        {
            if (((Vector2)activeMaze.transform.position - body.position).magnitude < 0.5)
            {
                body.position = activeMaze.transform.position;
                body.velocity = Vector3.zero;
                body.angularVelocity = 0;
                controlScript.zoomSpeed = 0.2f;
            }
        }
    }

    private void AddDescendantsWithTag(Transform parent, string tag, List<GameObject> list)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.tag == tag)
            {
                list.Add(child.gameObject);
            }
            AddDescendantsWithTag(child, tag, list);
        }
    }
}
