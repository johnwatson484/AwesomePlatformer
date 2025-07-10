using System.Collections;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    private float speed = 10f;
    private Animator animator;
    private bool canMove;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            Vector3 position = transform.position;
            position.x += speed * Time.deltaTime;
            transform.position = position;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == Tags.Beetle || target.gameObject.tag == Tags.Snail)
        {
            animator.Play("Explode");
            canMove = false;
            StartCoroutine(DisableBullet(0.1f));
        }
    }
}
