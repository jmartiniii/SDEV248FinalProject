using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadBounce : MonoBehaviour
{
    public float deathBounce;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * deathBounce, ForceMode2D.Impulse);
        //rb.AddForce(transform.eulerAngles = Vector3.forward * deathBounce);
        rb.AddTorque(200);
    }
}
