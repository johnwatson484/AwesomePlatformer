using System.Collections;
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

    public Transform downCollision;
    public Transform leftCollision;
    public Transform rightCollision;
    public Transform topCollision;

    private Vector3 frontOfSnail;
    private Vector3 backOfSnail;

    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        frontOfSnail = leftCollision.position;
        backOfSnail = rightCollision.position;
    }

    void Start()
    {
        moveLeft = true;
        canMove = true;
    }

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
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, playerLayer);
        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);

        if (topHit != null && topHit.gameObject.CompareTag(Tags.Player) && !stunned)
        {
            topHit.gameObject.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().linearVelocity.x, 7f);
            canMove = false;
            body.linearVelocity = new Vector2(0, 0);
            animator.Play("Stunned");
            stunned = true;

            if (CompareTag(Tags.Beetle))
            {
                StartCoroutine(Dead(0.5f));
            }
        }

        if (leftHit && leftHit.collider.gameObject.CompareTag(Tags.Player))
        {
            if (!stunned)
            {
                // Apply damage to the player
            }
            else if (!CompareTag(Tags.Beetle))
            {
                body.linearVelocity = new Vector2(15f, body.linearVelocity.y);
                StartCoroutine(Dead(3f));
            }
        }

        if (rightHit && rightHit.collider.gameObject.tag == Tags.Player)
        {
            if (!stunned)
            {
                // Apply damage to the player
            }
            else if (!CompareTag(Tags.Beetle))
            {
                body.linearVelocity = new Vector2(-15f, body.linearVelocity.y);
                StartCoroutine(Dead(3f));
            }
        }

        if (!Physics2D.Raycast(downCollision.position, Vector2.down, 0.1f))
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
            leftCollision.position = frontOfSnail;
            rightCollision.position = backOfSnail;
        }
        else
        {
            scale.x = -Mathf.Abs(scale.x);
            leftCollision.position = backOfSnail;
            rightCollision.position = frontOfSnail;
        }

        transform.localScale = scale;
    }
    
    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }
}
