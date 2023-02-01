using UnityEngine;

public class BugScript : MonoBehaviour
{
    public float speed, shootSpeed, range, reloadTime, HP;

    private Rigidbody2D enemy;
    private Transform transform;
    private Animator anim;
    private Collider2D col;

    private bool patrol, facingRight = true;

    private GameObject player;
    private Transform playerT;

    public GameObject bullet;
    public Transform shootPos;

    public bool isDead;
    public bool enemyShooter;

    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();

        isDead = false;

        player = GameObject.FindWithTag("Player");
        playerT = player.transform;

        patrol = true;

        HP = 10;
    }


    void Update()
    {
        if (HP <= 0)
        {
            isDead = true;
        }

        float distToPlayer = Vector2.Distance(transform.position, playerT.position);

        if (distToPlayer <= range)
        {
            patrol = false;
            transform.position = Vector2.MoveTowards(transform.position, playerT.position, Mathf.Abs(speed) * Time.deltaTime);
            if (playerT.position.x - 0.1f > transform.position.x && !facingRight)
            {
                Flip();
                
            }

            if (playerT.position.x + 0.1f < transform.position.x && facingRight)
            {
                Flip();
                
            }
        }
        if(distToPlayer >= range)
        {
            if (!patrol)
            {
                transform.position = Vector2.MoveTowards(transform.position, transform.parent.position, Mathf.Abs(speed) * Time.deltaTime);
            }
            if (transform.position == transform.parent.position)
            {
                patrol = true;
            }
            
                Patrol();
        }
    }

    void Patrol()
    {
        enemy.velocity = new Vector2(speed * 15f * Time.fixedDeltaTime, enemy.velocity.y);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        speed *= -1;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyTrigger")
        {
            //mustFlip = true;
            if (patrol)
            {
                Flip();
            }
            print("shouldFlip");
        }

        if (collision.CompareTag("Player"))
        {
            HP -= 10;
            speed = 0;
            //anim.SetTrigger("isDead");
            col.enabled = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Leaf")
        {
            HP -= 5;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}