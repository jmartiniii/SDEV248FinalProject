using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject player;
    public float speed;
    private float timer;
    private float attackTimer = 0f;
    private float distance;
    private float changeTime = 2.0f;
    float newMove;

    private AudioSource audioSource;
    public AudioClip attackSound;

    private SpriteRenderer rend;
    private Color newColor;
    private Color oldColor;

    int moveDir = 1;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        oldColor = rend.color;
        newColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            moveDir = -moveDir;
            timer = changeTime;

        }
        if (rb.velocity.x > 0)
        {
            Debug.Log("Moving left.");
        }
        else
        {
            Debug.Log(rb.velocity);
        }
        
    }

    private void FixedUpdate()
    {
        attackTimer -= Time.deltaTime;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan(direction.x) * Mathf.Rad2Deg;

        Vector2 position = rb.position;

        if (distance < 4)
        {
            if (attackTimer < 0)
            {
                audioSource.PlayOneShot(attackSound, .3f);
                attackTimer = changeTime;
            }
            rend.color = newColor;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 2 * speed * Time.deltaTime);
        }
        else
        {
            rend.color = oldColor;
            newMove = speed * moveDir * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + newMove, transform.position.y);
        }
    }
}
