using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttributes : MonoBehaviour {

    public float spd;
    public float maxSpd;
    public Rigidbody2D body;
    public float jumpPower;
    public float fallMultiplier;
    public float lowJumpMultiplier;

    public Transform groundCheck;

    private int score;
    public Text scoreText;
    public Text endText;
  
    public bool jump = false;

    private bool facingRight = true;
    private bool onGround;

    // Use this for initialization
    void Start () {
        onGround = true;
        score = 0;
        SetText(scoreText, "Score: " + score);
        SetText(endText, "");
    }

    private bool GroundCheck()
    {
        return Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            jump = true;
        }
    }

    private void FixedUpdate()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            GetComponent<CustomGravity>().ReverseGravity();

            Debug.Log("Vaihetaan gravityn suuntaa");
        }

        onGround = GroundCheck();
      
        if (transform.position.y < -85)
            SetText(endText, "YOU DIED!\nYOUR SCORE: " + score);

        float xMove = Input.GetAxis("Horizontal");

        MoveHorizontal();
        MoveVertical();

        if (xMove > 0 && !facingRight)
        {
            flip();
        } else if (xMove < 0 && facingRight)
        {
            flip();
        }
        
	}

    private void MoveVertical()
    {
        if (jump)
        {
            body.velocity = Vector2.up * jumpPower * Mathf.Sign(Physics2D.gravity.y);
            //TODO ota huomioon et jos gravitaatio ei oo kokonaan ylöspäin, eli sillon hyppy ei saa olla yhtä voimakas 
            //ja sen pitäis olla myös sivuttain
            jump = false;
        }

        //This makes the jumping feel a lot better
        if (body.velocity.y < 0)
        {
            //Increase falling speed
            body.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        }
        else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            //Make it possible to hold jump button for longer
            body.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }
    }

    private void MoveHorizontal()
    {

        float xMove = Input.GetAxis("Horizontal");

        if (xMove * body.velocity.x < maxSpd)
            body.AddForce(Vector2.right * xMove * spd);

        if (Mathf.Abs(body.velocity.x) > maxSpd)
            body.velocity = new Vector2(Mathf.Sign(body.velocity.x) * maxSpd, body.velocity.y);

    }

    void flip()
    {
        facingRight = !facingRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            score += 100;
            SetText(scoreText, "Score: " + score);
        }
    }

    void SetText(Text textObject, string text)
    {
        textObject.text = text;
    }

}
