using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class RotateGameWorld : MonoBehaviour {
    
    private List<GameObject> activePlatforms = new List<GameObject>();
    private List<GameObject> activeMemories = new List<GameObject>();
    public GameObject activeMaze;

    private GameObject[] collectables;
    private Quaternion[] rotations;
    
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

    public bool rotating;
    private Vector3 rotateDirection;
    private float rotated = 0;

    public AudioClip rotateSound;

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
        collectables = GameObject.FindGameObjectsWithTag("Coin");
        rotations = collectables.Select(c => c.transform.rotation).ToArray();
        foreach(GameObject platform in activePlatforms)
        {
            platform.transform.RotateAround(activeMaze.transform.position, direction, rotationSpeed);
        }
        foreach(GameObject memory in activeMemories)
        {
            memory.transform.RotateAround(activeMaze.transform.position, direction, rotationSpeed);
        }
        for(int i = 0; i < rotations.Count(); i++)
        {
            collectables[i].transform.rotation = rotations[i];
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
        if (Input.GetKeyDown(KeyCode.Q) && BodyInCenter(body) && !rotating)
        {
            rotating = true;
            rotateDirection = new Vector3(0, 0, -1);
            SoundManager.instance.PlaySfx(rotateSound);
        }
        if (Input.GetKeyDown(KeyCode.E) && BodyInCenter(body) && !rotating)
        {
            rotating = true;
            rotateDirection = new Vector3(0, 0, 1);
            SoundManager.instance.PlaySfx(rotateSound);
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
            ChangeColliders();
            
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

    private void ChangeColliders()
    {
        GameObject[] plats = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject platform in plats)
        {
            platform.GetComponent<PolygonCollider2D>().enabled = !platform.GetComponent<PolygonCollider2D>().enabled;
        }
    }

    public void SetActivePlatforms()
    {
        if(activeMaze != null)
        {
            activePlatforms.Clear();
            activeMemories.Clear();
            if (activeMaze.tag == "center" && ringNumber == maxRing)
            {
                Transform mazes = GameObject.FindGameObjectWithTag("Mazes").transform;
                foreach (Transform maze in mazes)
                {
                    if(maze.gameObject.tag != "center")
                    {
                        activePlatforms.Add(maze.gameObject);
                    }
                }
            }
            else
            {
                activePlatforms.Add(activeMaze.transform.Find("Offset").Find("Blocks").Find("Ring" + ringNumber).gameObject);
                activeMemories.Add(activeMaze.transform.Find("Collectables").Find("Ring" + ringNumber).gameObject);
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
