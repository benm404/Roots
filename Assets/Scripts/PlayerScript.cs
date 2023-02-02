using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    GameManager GM;
    SceneSwitcher SM;

    Transform tr;
    Rigidbody2D rb;
    Collider2D col;

    public float Speed = 10f;
    public float maxSpeed = 1f;
    public float JumpHeight = 10f;

    [SerializeField] private bool grounded = false;
    public bool climb = false;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        GM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        SM = GameObject.FindWithTag("SceneSwitcher").GetComponent<SceneSwitcher>();
    }


    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (Mathf.Abs(rb.velocity.x) <= maxSpeed)
        {
            rb.AddForce(new Vector2(Speed * move * Time.deltaTime, 0f));
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector2(0f, JumpHeight), ForceMode2D.Impulse);
        }

        if(climb)
        {
            rb.gravityScale = 0f;
            if (Mathf.Abs(rb.velocity.y) <= maxSpeed)
            {
                rb.AddForce(new Vector2(0f, Speed * moveY * Time.deltaTime));
            }
        } else { rb.gravityScale = 2f; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            SM.SceneSwitch(3);
        }
    }
}
