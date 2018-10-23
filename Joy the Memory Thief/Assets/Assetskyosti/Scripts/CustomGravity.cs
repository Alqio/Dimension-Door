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
        Vector2 newGravity = gravity;
        Physics2D.gravity = newGravity;
    }

    public void ReverseGravity()
    {
        gravity = new Vector2(gravity.x, -gravity.y);
    }

    public void ChangeGravityDirection()
    {
        gravity = new Vector2(-gravity.y, gravity.x);
        Debug.Log(gravity);
    }

}

