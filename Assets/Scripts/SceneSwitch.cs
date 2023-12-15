using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip cashAudio;
    Animator myAnim;
    const string PRESS_ANIM = "PlayerTouch";
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(cashAudio, 0.2f);
            myAnim.SetTrigger(PRESS_ANIM);
        }
    }
}
