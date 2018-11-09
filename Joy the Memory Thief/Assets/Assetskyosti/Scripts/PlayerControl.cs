using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [HideInInspector]
    public Rigidbody2D body;
    private Camera mainCamera;
    public bool jump = false;
    public bool toCenter = false;
    public bool canMove;

    PlayerAttributes attributes;
    private bool onGround;
    
    private bool facingRight = false;

    
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
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 13);
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 20);
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 27);
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 34);
        Gizmos.DrawWireSphere(new Vector3(0, 0, 0), 41);

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

        if (GetComponent<RotateGameWorld>())
        {
            GetComponent<RotateGameWorld>().HandleInput(body);
        }
        
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            return;
        }
        if (transform.position.y < -85)
        {
            attributes.SetText(attributes.endText, "Level 1 cleared");
        }
        
        MoveVertical();

        if (toCenter)
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
