using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D body;
    private Animator animator;

    public Transform groundCheckPosition;
    public LayerMask groundLayer;

    private bool isGrounded;
    private bool jumped;
    private float jumpPower = 5f;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        Jump();
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            body.linearVelocity = new Vector2(speed, body.linearVelocity.y);
            ChangeDirection((int)h);
        }
        else if (h < 0)
        {
            body.linearVelocity = new Vector2(-speed, body.linearVelocity.y);
            ChangeDirection((int)h);
        }
        else
        {
            body.linearVelocity = new Vector2(0, body.linearVelocity.y);
        }

        animator.SetInteger("Speed", Mathf.Abs((int)body.linearVelocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = direction;
        transform.localScale = scale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);

        if (isGrounded && jumped)
        {
            jumped = false;
            animator.SetBool("Jump", false);
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            jumped = true;
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            animator.SetBool("Jump", true);
        }
    }
}
