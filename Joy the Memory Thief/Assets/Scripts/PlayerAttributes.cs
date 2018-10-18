using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerAttributes : MonoBehaviour {

    public float spd;
    public Rigidbody2D body;
    public float jumpPower;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    private int score;
    public Text scoreText;
    public Text endText;
  
    public bool jump = false;

    private bool facingRight = true;
    private bool onGround;

    private float dist = 1.4f;

    // Use this for initialization
    void Start () {
        onGround = true;
        score = 0;
        SetText(scoreText, "Score: " + score);
        SetText(endText, "");
    }
  
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Vector3 c_extents = GetComponent<Collider2D>().bounds.extents;
        Vector3 startPos = transform.position - c_extents;
        startPos += new Vector3(0.2f, -0.01f);
        Vector3 newPos = new Vector3(startPos.x + c_extents.x * 2 - 0.2f*2, startPos.y);

        Gizmos.DrawLine(startPos, newPos);
        
    }

    private void GroundCheck()
    {
        Vector3 c_extents = GetComponent<Collider2D>().bounds.extents;
        Vector3 startPos = transform.position - c_extents;
        
        Vector3 newPos = new Vector3(startPos.x + c_extents.x * 2, startPos.y);

        RaycastHit2D hit = Physics2D.Raycast(startPos, newPos);
        
        if (hit)
        {
            if (hit.collider.CompareTag("Block") && !onGround)
            {
                onGround = true;
                Debug.Log("osuttiin maaha");
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            jump = true;
            onGround = false;
        }
    }
    private void FixedUpdate()
    {
        GroundCheck();
      
        if (transform.position.y < -85)
            SetText(endText, "YOU DIED!\nYOUR SCORE: " + score);

        float xMove = Input.GetAxis("Horizontal");
        if (xMove != 0)
        {
            body.transform.Translate(new Vector3(xMove * spd * Time.deltaTime, 0, 0));
        }

        if (xMove > 0 && !facingRight)
        {
            flip();
        } else if (xMove < 0 && facingRight)
        {
            flip();
        }

        if (jump)
        {
            body.velocity = Vector2.up * jumpPower;
            jump = false;
        }

        //This makes the jumping feel a lot better
        if (body.velocity.y < 0)
        {
            //Increase falling speed
            body.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
        } else if (body.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            //Make it possible to hold jump button for longer
            body.velocity += Vector2.up * Physics2D.gravity.y * lowJumpMultiplier * Time.deltaTime;
        }

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
