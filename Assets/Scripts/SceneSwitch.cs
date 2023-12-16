using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip chestAudio;
    private Animator animator;
    const string chestClose = "PlayerTouch";
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // trigger chest sound and animation
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(chestAudio, 0.2f);
            animator.SetTrigger(chestClose);
        }
    }
}
