using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D body;
    private Animator animator;

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
        
    }

    void FixedUpdate()
    {
        Walk();
    }

    void Walk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0)
        {
            body.linearVelocity = new Vector2(speed, body.linearVelocity.y);
        }
        
        if(h < 0)
        {
            body.linearVelocity = new Vector2(-speed, body.linearVelocity.y);
        }
    }
}
