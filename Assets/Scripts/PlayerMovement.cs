using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;
    private bool playerActive;

    public AudioClip jumpSound;
    private AudioSource playerAudio;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private void Start()
    {
        playerActive = true;
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerMove();
        PlayerJump();
        PlayerFlip();
        PlayerActive();        
    }

    private void PlayerMove()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerAudio.PlayOneShot(jumpSound, 0.5f);
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void PlayerFlip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void PlayerActive()
    {
        if (playerActive == false)
        {
            speed = 0f;
            jumpingPower = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SceneSwitch")) {
            playerActive = false;
        }
    }
}
