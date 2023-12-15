using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // enemy target
    public GameObject playerTarget;

    // movement
    private float newMove, distance, moveTimer;
    private float moveSpeed = 1.0f;
    private float changeTime = 2.0f;
    private float moveDir = 1.0f;
    
    // attack
    private float attackTimer = 0f;
    private float attackReset = 2.0f;
    private float attackRange = 4.0f;
    
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
        moveTimer -= Time.deltaTime;
        if (moveTimer < 0)
        {
            ChangeDirection();
        }
    }

    private void FixedUpdate()
    {
        GetDistance();
        
        if (distance < attackRange)
        {
            AttackMove();
        }
        else
        {
            BasicMove();
        }
    }

    private void GetDistance()
    {
        distance = Vector2.Distance(transform.position, playerTarget.transform.position);
    }

    private void AttackMove()
    {
        attackTimer -= Time.deltaTime;
        Vector2 direction = playerTarget.transform.position - transform.position;
        direction.Normalize();

        animator.SetFloat("moveLeft", -direction.x);
        if (attackTimer < 0)
        {
            audioSource.PlayOneShot(attackSound, .3f);
            attackTimer = attackReset;
        }
        spriteRender.color = newColor;
        transform.position = Vector2.MoveTowards(this.transform.position, playerTarget.transform.position, 2 * moveSpeed * Time.deltaTime);
    }

    private void BasicMove()
    {
        animator.SetFloat("moveLeft", -moveDir);
        spriteRender.color = oldColor;
        newMove = moveSpeed * moveDir * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + newMove, transform.position.y);
    }

    private void ChangeDirection()
    {
        moveDir = -moveDir;
        moveTimer = changeTime;
    }
}
