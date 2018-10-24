using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    [HideInInspector]
    public Rigidbody2D body;

    PlayerAttributes attributes;
    private bool onGround;
    public bool jump = false;

    private bool facingRight = false;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        onGround = true;
        attributes = GetComponent<PlayerAttributes>();
        flip();
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
        if (Input.GetButtonDown("Jump") && onGround)
        {
            jump = true;
        }

        if (Input.GetKey(KeyCode.E))
        {
            GetComponent<RotateGameWorld>().Rotate(new Vector3(0, 0, -1));
        }

        if (Input.GetKey(KeyCode.Q))
        {
            GetComponent<RotateGameWorld>().Rotate(new Vector3(0, 0, 1));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<CustomGravity>().ChangeGravityDirection();
        }
    }

    private void FixedUpdate()
    {

        //float zRot = Mathf.Min(30f, 10 * Mathf.Abs(body.velocity.x)) * -Mathf.Sign(body.velocity.x);
        //Debug.Log(zRot);
        
        //body.MoveRotation(body.rotation * zRot*Time.fixedDeltaTime);


        if (transform.position.y < -85)
        {
            attributes.SetText(attributes.endText, "YOU DIED!\nYOUR SCORE: " + attributes.score);
        }
        
        float xMove = Input.GetAxis("Horizontal");

        MoveHorizontal();
        MoveVertical();

        if (xMove > 0 && !facingRight)
        {
            flip();
        }
        else if (xMove < 0 && facingRight)
        {
            flip();
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
       
    }


    private void MoveVertical()
    {
        if (jump)
        {
            //body.AddForce(new Vector2(0f, jumpPower * 20f));

            body.velocity = Vector2.up * attributes.jumpPower * -Mathf.Sign(Physics2D.gravity.y);

            //TODO ota huomioon et jos gravitaatio ei oo kokonaan ylöspäin, eli sillon hyppy ei saa olla yhtä voimakas 
            //ja sen pitäis olla myös sivuttain
            jump = false;
        }

        if (body.velocity.y < 0)
        {
            //Increase falling speed
            body.velocity += Vector2.up * Physics2D.gravity.y * attributes.fallMultiplier * Time.fixedDeltaTime;
            //body.velocity += Vector2.right * Physics2D.gravity.x * fallMultiplier * Time.fixedDeltaTime;

        }
        else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            //Make it possible to hold jump button for longer
            body.velocity += Vector2.up * Physics2D.gravity.y * attributes.lowJumpMultiplier * Time.fixedDeltaTime;
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
