using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseBounce : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * RandomizeBounce(), ForceMode2D.Impulse);
        rb.AddForce(transform.right * RandomizeLeftRight(), ForceMode2D.Impulse);
        rb.AddTorque(RandomizeTorque());
    }

    private float RandomizeLeftRight()
    {
        float maxLeft = -45.0f;
        float maxRight = 45.0f;
        return Random.Range(maxLeft, maxRight);
    }

    private float RandomizeBounce()
    {
        float minBounce = 15.0f;
        float maxBounce = 30.0f;
        return Random.Range(minBounce, maxBounce);
    }

    private float RandomizeTorque()
    {
        float minTorque = -800.0f;
        float maxTorque = 800.0f;
        return Random.Range(minTorque, maxTorque);
    }
}
