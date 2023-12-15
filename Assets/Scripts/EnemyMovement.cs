using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    private float speed = 1;
    private float timer;
    private float attackTimer = 0f;
    private float distance;
    private float changeTime = 2.0f;
    private float newMove;

    private AudioSource audioSource;
    public AudioClip attackSound;

    private SpriteRenderer rend;
    private Color newColor;
    private Color oldColor;
    private Animator animator;

    int moveDir = 1;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
    }

    private void FixedUpdate()
    {
        attackTimer -= Time.deltaTime;
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        

        if (distance < 4)
        {
            animator.SetFloat("moveLeft", -direction.x);
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
            animator.SetFloat("moveLeft", -moveDir);
            rend.color = oldColor;
            newMove = speed * moveDir * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + newMove, transform.position.y);
        }
    }
}
