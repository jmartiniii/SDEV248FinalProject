using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // enemy target
    public GameObject playerTarget;

    // movement
    private float distance, moveTimer;
    private float moveSpeed = 1.0f;
    private float changeTime = 2.0f;
    private float moveDir = 1.0f;
    
    // attack
    private float attackTimer = 0f;
    
    // audio
    private AudioSource audioSource;
    public AudioClip attackSound;

    // sprite, colors, animation
    private SpriteRenderer spriteRender;
    private Color newColor, oldColor;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        moveTimer = changeTime;
        oldColor = spriteRender.color;
        newColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTimer();
        AttackTimer();
    }

    private void FixedUpdate()
    {
        GetDistance();
        DecideMove();
    }

    private void MoveTimer()
    {
        // maintain timer so that enemy knows when to move left or right
        moveTimer -= Time.deltaTime;
        if (moveTimer < 0)
        {
            ChangeDirection();
        }
    }

    private void GetDistance()
    {
        // determine the distance between the enemy and the player
        distance = Vector2.Distance(transform.position, playerTarget.transform.position);
    }

    private void AttackMove()
    {
        float attackReset = 2.0f;
        // determine the direction to move
        Vector2 direction = playerTarget.transform.position - transform.position;
        direction.Normalize();

        // change the way the enemy is looking
        animator.SetFloat("moveLeft", -direction.x);

        // if timer is < 0 then play sound and reset timer
        if (attackTimer < 0)
        {
            audioSource.PlayOneShot(attackSound, .3f);
            attackTimer = attackReset;
        }
        // turn red and move towards the player at increased speed
        spriteRender.color = newColor;
        transform.position = Vector2.MoveTowards(this.transform.position, playerTarget.transform.position, 2 * moveSpeed * Time.deltaTime);
    }

    private void BasicMove()
    {
        // move back and forth based on timer and direction
        animator.SetFloat("moveLeft", -moveDir);
        spriteRender.color = oldColor;
        float newMove = moveSpeed * moveDir * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + newMove, transform.position.y);
    }

    private void DecideMove()
    {
        float attackRange = 4.0f;

        if (distance < attackRange)
        {
            AttackMove();
        }
        else
        {
            BasicMove();
        }
    }

    private void ChangeDirection()
    {
        // change left/right based on timer
        moveDir = -moveDir;
        moveTimer = changeTime;
    }

    private void AttackTimer()
    {
        // time between attack sounds so it isn't infinite
        attackTimer -= Time.deltaTime;
    }
}
