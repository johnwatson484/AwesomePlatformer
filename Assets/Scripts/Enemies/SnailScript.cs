using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D body;
    private Animator animator;
    private bool moveLeft;

    public Transform down_Collision;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
        {
            body.linearVelocity = new Vector2(-speed, body.linearVelocity.y);
        }
        else
        {
            body.linearVelocity = new Vector2(speed, body.linearVelocity.y);
        }

        CheckCollision();
    }

    void CheckCollision()
    {
        if (!Physics2D.Raycast(down_Collision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;

        Vector3 scale = transform.localScale;

        if (moveLeft)
        {
            scale.x = Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
        }

        transform.localScale = scale;
    }
}
