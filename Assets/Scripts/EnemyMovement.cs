using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject player;
    private float moveSpeed = 1;
    private float moveTimer;
    private float attackTimer = 0f;
    private float attackReset = 2.0f;
    private float distance;
    private float changeTime = 2.0f;
    private float newMove;
    private float attackRange = 4.0f;
    private float moveDir = 1.0f;

    private AudioSource audioSource;
    public AudioClip attackSound;

    private SpriteRenderer spriteRender;
    private Color newColor;
    private Color oldColor;
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
        distance = Vector2.Distance(transform.position, player.transform.position);
    }

    private void AttackMove()
    {
        attackTimer -= Time.deltaTime;
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();

        animator.SetFloat("moveLeft", -direction.x);
        if (attackTimer < 0)
        {
            audioSource.PlayOneShot(attackSound, .3f);
            attackTimer = attackReset;
        }
        spriteRender.color = newColor;
        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 2 * moveSpeed * Time.deltaTime);
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
