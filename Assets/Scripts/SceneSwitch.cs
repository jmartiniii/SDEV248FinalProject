using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitch : MonoBehaviour
{
    Animator myAnim;
    const string PRESS_ANIM = "PlayerTouch";
    
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            myAnim.SetTrigger(PRESS_ANIM);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            myAnim.SetTrigger(PRESS_ANIM);
        }
    }
}
