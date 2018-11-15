using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [HideInInspector]
    public Rigidbody2D body;
    private Camera mainCamera;
    public bool jump = false;
    public bool toCenter = false;
    public bool canMove = true;

    PlayerAttributes attributes;
    private bool onGround;
    
    private bool facingRight = false;

    public float zoomSpeed = 0.2f;
    public float maxZoomSpeed = 20f;
    private float originalZoomSpeed;
    public float targetZoom = 8f;

    public AudioClip jumpClip;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        attributes = GetComponent<PlayerAttributes>();
    }

    // Use this for initialization
    void Start () {
        Physics2D.IgnoreLayerCollision(0, 9);
        canMove = true;
        originalZoomSpeed = zoomSpeed;
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

        Gizmos.DrawWireSphere(new Vector3(0, -68, 0), 13);
        Gizmos.DrawWireSphere(new Vector3(0, -68, 0), 20);
        Gizmos.DrawWireSphere(new Vector3(0, -68, 0), 27);
        Gizmos.DrawWireSphere(new Vector3(0, -68, 0), 34);

        Gizmos.DrawWireSphere(new Vector3(0, 68, 0), 13);
        Gizmos.DrawWireSphere(new Vector3(0, 68, 0), 20);
        Gizmos.DrawWireSphere(new Vector3(0, 68, 0), 27);
        Gizmos.DrawWireSphere(new Vector3(0, 68, 0), 34);

        Gizmos.DrawWireSphere(new Vector3(68, 0, 0), 13);
        Gizmos.DrawWireSphere(new Vector3(68, 0, 0), 20);
        Gizmos.DrawWireSphere(new Vector3(68, 0, 0), 27);
        Gizmos.DrawWireSphere(new Vector3(68, 0, 0), 34);

        Gizmos.DrawWireSphere(new Vector3(-68, 0, 0), 13);
        Gizmos.DrawWireSphere(new Vector3(-68, 0, 0), 20);
        Gizmos.DrawWireSphere(new Vector3(-68, 0, 0), 27);
        Gizmos.DrawWireSphere(new Vector3(-68, 0, 0), 34);


    }

    private bool GroundCheck()
    {
        return Physics2D.Linecast(transform.position, attributes.groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
    void Update () {
        onGround = GroundCheck();
        HandleInput();
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

    public void ResetZoomSpeed()
    {
        zoomSpeed = originalZoomSpeed;
    }

    private void FixedUpdate()
    {
        //Vector3 oldRotation = body.transform.eulerAngles;

        //float newZRotation = Mathf.Min(10f, 20f * Mathf.Abs(body.velocity.x)) * -Mathf.Sign(body.velocity.x);

        //Debug.Log(newZRotation);

        //body.transform.eulerAngles = new Vector3(oldRotation.x, oldRotation.y, newZRotation);

        if (!canMove)
        {
            return;
        }
        if (transform.position.y < -85)
        {
            attributes.SetText(attributes.endText, "Level 1 cleared");
        }
        
        MoveVertical();

        if (!toCenter)
        {
            MoveHorizontal();
        }
        if (mainCamera.orthographicSize < targetZoom - 0.1 || mainCamera.orthographicSize > targetZoom + 0.1)
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
            //SoundManager.instance.PlaySfx(jumpClip);
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

    void Zoom()
    {
        if (mainCamera.orthographicSize < targetZoom)
        {
            mainCamera.orthographicSize += zoomSpeed;

            //Makes sure that the orthographicSize doesn't overextend
            if (mainCamera.orthographicSize > targetZoom)
            {
                mainCamera.orthographicSize = targetZoom;
            }

        }
        else if (mainCamera.orthographicSize > targetZoom)
        {
            mainCamera.orthographicSize -= zoomSpeed;

            //Makes sure that the orthographicSize doesn't overextend
            if (mainCamera.orthographicSize < targetZoom)
            {
                mainCamera.orthographicSize = targetZoom;
            }
        }

        

        if (Mathf.Abs(mainCamera.orthographicSize - targetZoom) > 4)
        {
            zoomSpeed = Mathf.Min(zoomSpeed * 1.1f, maxZoomSpeed);
        } else
        {
            zoomSpeed = Mathf.Max(zoomSpeed / 1.1f, originalZoomSpeed);
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
