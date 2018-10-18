using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    private Vector2 gravity;
    private Vector2 velocity;
    private Rigidbody2D body;

    public float gravityModifier = 1f;

    public float dir = 0f;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        gravity = new Vector2(0f, -9.81f);
    }

    void FixedUpdate()
    {
        //body.velocity = new Vector2(body.velocity.x + gravity.x * Time.fixedDeltaTime, body.velocity.y + gravity.y * Time.fixedDeltaTime) * gravityModifier;

        Vector2 newGravity = gravity;
        Physics2D.gravity = newGravity;

        Debug.Log(Physics2D.gravity);
    }

    public void ReverseGravity()
    {
        gravity = new Vector2(gravity.x, -gravity.y);
    }

    public void ChangeGravityDirection(float dir)
    {
        //todo
    }

}

