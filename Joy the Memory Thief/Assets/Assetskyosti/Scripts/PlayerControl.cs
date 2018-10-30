using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [HideInInspector]
    public Rigidbody2D body;
    private Camera mainCamera;
    public bool jump = false;

    PlayerAttributes attributes;
    private bool onGround;
    
    private bool facingRight = false;

    private bool toCenter = false;
    public float targetZoom = 4f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        attributes = GetComponent<PlayerAttributes>();
    }

    // Use this for initialization
    void Start () {
        onGround = true;
        mainCamera = Camera.main;
        flip();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 15);
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 25);
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 40);
    }

    private bool GroundCheck()
    {
        return Physics2D.Linecast(transform.position, attributes.groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
    void Update () {
        onGround = GroundCheck();

        HandleInput();
        
        Vector3 oldRotation = body.transform.eulerAngles;
        
        float newZRotation = Mathf.Min(30f, 20f*Mathf.Abs(body.velocity.x)) * -Mathf.Sign(body.velocity.x);

        //Debug.Log(newZRotation);

        body.transform.eulerAngles = new Vector3(oldRotation.x, oldRotation.y, newZRotation);

    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && onGround)
        {
            jump = true;
        }

        if (Input.GetKey(KeyCode.Q) && body.position == Vector2.zero)
        {
            GetComponent<RotateGameWorld>().Rotate(new Vector3(0, 0, -1));
        }

        if (Input.GetKey(KeyCode.E) && body.position == Vector2.zero)
        {
            GetComponent<RotateGameWorld>().Rotate(new Vector3(0, 0, 1));
        }

        if (Input.GetKeyDown(KeyCode.W) && Mathf.Abs(body.position.x) < 5.5f && Mathf.Abs(body.position.y) < 5.5f)
        {
            toCenter = !toCenter;
            if(toCenter)
            {
                body.velocity = new Vector2((float)-body.position.x, (float)-body.position.y);
                body.gravityScale = 0;
                targetZoom = 24f;
            } else
            {
                body.gravityScale = 1;
                targetZoom = 8f;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && body.position == Vector2.zero)
        {
            GetComponent<RotateGameWorld>().DecreaseRing();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && body.position == Vector2.zero)
        {
            GetComponent<RotateGameWorld>().IncreaseRing();
        }
        /*
        for(int i = 0; i < 3; i++)
        {
            Debug.Log("ring" + i);
            GameObject[] platforms1 = GameObject.FindGameObjectsWithTag("Ring" + i);
            foreach (GameObject platform in platforms1)
            {
                platform.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Ring" + GetComponent<RotateGameWorld>().ringNumber);
        foreach (GameObject platform in platforms)
        {
            Debug.Log("moi");
            platform.GetComponent<SpriteRenderer>().color = Color.green;
        }
         */
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -85)
        {
            attributes.SetText(attributes.endText, "Level 1 cleared");
        }
        
        MoveVertical();

        if(toCenter)
        {
            MoveTowardsCenter();
        } else
        {
            MoveHorizontal();
        }
        if (mainCamera.orthographicSize < targetZoom - 0.1 || mainCamera.orthographicSize > targetZoom + 1)
        {
            Zoom();
        }
    }

    private void MoveHorizontal()
    {
        float xMove = Input.GetAxis("Horizontal");

        float maxSpeed = attributes.maxSpd;
        float h = xMove;
        Rigidbody2D rb2d = body;

        float moveForce = 365f;

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);


        if (xMove > 0 && !facingRight)
        {
            flip();
        }
        else if (xMove < 0 && facingRight)
        {
            flip();
        }
    }


    private void MoveVertical()
    {

        if (jump)
        {
            body.velocity = Vector2.up * attributes.jumpPower * -Mathf.Sign(Physics2D.gravity.y);
            jump = false;
        }

        if (body.velocity.y < 0 && !toCenter)
        {
            //Increase falling speed
            body.velocity += Vector2.up * Physics2D.gravity.y * attributes.fallMultiplier * Time.fixedDeltaTime;

        }
        else if (body.velocity.y > 0 && !Input.GetButton("Jump") && !toCenter)
        {
            //Make it possible to hold jump button for longer
            body.velocity += Vector2.up * Physics2D.gravity.y * attributes.lowJumpMultiplier * Time.fixedDeltaTime;
        }
        
    }

    void MoveTowardsCenter()
    {
        if (toCenter)
        {
            if (Mathf.Abs(body.position.x) < 0.5 && Mathf.Abs(body.position.y) < 0.5)
            {
                body.position = Vector2.zero;
                body.velocity = Vector3.zero;
                body.angularVelocity = 0;
            }
        }
    }

    void Zoom()
    {
        if (mainCamera.orthographicSize < targetZoom)
        {
            mainCamera.orthographicSize += 0.2f;
        }
        else if (mainCamera.orthographicSize > targetZoom)
        {
            mainCamera.orthographicSize -= 0.2f;
        }


    }

    void flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


}
