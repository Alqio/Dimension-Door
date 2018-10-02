using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour {

    public float spd;
    public Rigidbody2D body;
    public float jumpPower;
    public Transform groundCheck;

    public bool jump = false;

    private bool facingRight = true;
    private bool onGround;

    // Use this for initialization
    void Start () {
        onGround = true;
	}

    // Update is called once per frame
    void Update() {
        //onGround = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        print("onGround: " + onGround);

        if (body.velocity.y <= 0)
        {
            jump = false;
        }

        if (Input.GetButtonDown("Jump") && !jump)
        {
            print("jumping pls");
            jump = true;
        }
    }
    private void FixedUpdate()
    {
        float xMove = Input.GetAxis("Horizontal");
        if (xMove != 0)
        {
            body.transform.Translate(new Vector3(xMove * spd * Time.deltaTime, 0, 0));
            //body.AddForce(new Vector2(xMove * spd * Time.deltaTime, 0));
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
            body.AddForce(Vector2.up * jumpPower * Time.deltaTime);
            jump = false;
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
