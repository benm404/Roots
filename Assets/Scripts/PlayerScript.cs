using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    GameManager GM;

    Transform tr;
    Rigidbody2D rb;
    Collider2D col;

    public float Speed = 10f;
    public float maxSpeed = 1f;
    public float JumpHeight = 10f;

    [SerializeField] private bool grounded = false;

    void Start()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        GM = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }


    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        if (Mathf.Abs(rb.velocity.x) <= maxSpeed)
        {
            rb.AddForce(new Vector2(Speed * move, 0f));
        }
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector2(0f, JumpHeight), ForceMode2D.Impulse);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}
