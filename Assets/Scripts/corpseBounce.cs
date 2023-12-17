using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corpseBounce : MonoBehaviour
{
    private float bounceForce = 25.0f;
    private float torqueForce = 200.0f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bounceForce, ForceMode2D.Impulse);
        rb.AddTorque(torqueForce);
    }
}
