using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;

    private float horizontal;
    private float moveSpeed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool playerActive = true;

    private AudioSource playerAudio;
    public AudioClip jumpSound;

    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        JumpFlip();
    }

    private void LateUpdate()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        if (playerActive)
        {
            // move the player
            horizontal = Input.GetAxisRaw("Horizontal");
            rigidBody.velocity = new Vector2(horizontal * moveSpeed, rigidBody.velocity.y);
        }
        else
        {
            // disable movement
            rigidBody.velocity = Vector3.zero;
        }
        
    }

    private void JumpFlip()
    {
        if (playerActive)
        {
            PlayerJump();
            PlayerFlip();
        }
    }

    private void PlayerJump()
    {
        // make the player jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerAudio.PlayOneShot(jumpSound, 0.5f);
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rigidBody.velocity.y > 0f)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        // is the player on the ground
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void PlayerFlip()
    {
        // look left or right depending on directions
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            gameManager.Die();
        }

        else if (collision.CompareTag("Coin"))
        {
            // add coin
            Destroy(collision.gameObject);
            gameManager.AddToCoinTotal(1);
        }

        else if (collision.CompareTag("Coin5"))
        {
            // add coins
            Destroy(collision.gameObject);
            gameManager.AddToCoinTotal(5);
        }

        else if (collision.CompareTag("SceneSwitch"))
        {
            playerActive = false;
            // switch scene
            gameManager.UpdateTotals();
            StartCoroutine(gameManager.SceneSwitch());
        }

    }
}
