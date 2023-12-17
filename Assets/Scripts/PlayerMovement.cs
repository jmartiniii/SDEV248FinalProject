using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float moveSpeed = 8f;
    private float jumpingPower = 16f;
    private bool isFacingRight;
    private bool playerActive;

    public AudioClip jumpSound;
    private AudioSource playerAudio;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;


    private void Start()
    {
        isFacingRight = true;
        playerActive = true;
        playerAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayerActive();
    }

    private void PlayerActive()
    {
        // if the player has touched the chest, disable movement
        if (playerActive)
        {
            PlayerMove();
            PlayerJump();
            PlayerFlip();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void PlayerMove()
    {
        // move the player
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    private void PlayerJump()
    {
        // make the player jump
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
        // disable the player after touching the scene switch chest
        if (collision.CompareTag("SceneSwitch")) {
            playerActive = false;
        }
    }
}
