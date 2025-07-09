using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody2D body;
    private Animator animator;

    public LayerMask playerLayer;

    private bool moveLeft;

    private bool canMove;
    private bool stunned;

    public Transform down_Collision;
    public Transform left_Collision;
    public Transform right_Collision;
    public Transform top_Collision;

    private Vector3 frontOfSnail;
    private Vector3 backOfSnail;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        frontOfSnail = left_Collision.position;
        backOfSnail = right_Collision.position;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                body.linearVelocity = new Vector2(-speed, body.linearVelocity.y);
            }
            else
            {
                body.linearVelocity = new Vector2(speed, body.linearVelocity.y);
            }
        }
        CheckCollision();
    }

    void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);
        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

        if (topHit != null && topHit.gameObject.tag == "Player" && !stunned)
        {
            topHit.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().linearVelocity.x, 7f);
            canMove = false;
            body.linearVelocity = new Vector2(0, 0);
            animator.Play("Stunned");
            stunned = true;
        }

        if (leftHit && leftHit.collider.gameObject.tag == "Player")
        {
            if (!stunned)
            {
                // Apply damage to the player
            }
            else
            {
                body.linearVelocity = new Vector2(15f, body.linearVelocity.y);
            }
        }
        
        if (rightHit && rightHit.collider.gameObject.tag == "Player")
        {
            if (!stunned)
            {
                // Apply damage to the player
            }
            else
            {
                body.linearVelocity = new Vector2(-15f, body.linearVelocity.y);
            }
        }
        
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
            left_Collision.position = frontOfSnail;
            right_Collision.position = backOfSnail;
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
            left_Collision.position = backOfSnail;
            right_Collision.position = frontOfSnail;
        }

        transform.localScale = scale;
    }
}
