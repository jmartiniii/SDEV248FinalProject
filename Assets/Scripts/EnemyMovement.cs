using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject player;
    public float speed;
    private float timer;
    private float distance;
    private float changeTime = 2.0f;
    float newMove;

    public SpriteRenderer rend;
    public Color newColor;
    public Color oldColor;

    int moveDir = 1;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        timer = changeTime;
        oldColor = rend.color;
        newColor = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            moveDir = -moveDir;
            timer = changeTime;

        }
        
    }

    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan(direction.x) * Mathf.Rad2Deg;

        Vector2 position = rb.position;

        if (distance < 4)
        {
            rend.color = newColor;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, 2 * speed * Time.deltaTime);
        }
        else
        {
            rend.color = oldColor;
            newMove = speed * moveDir * Time.deltaTime;
            transform.position = new Vector3(transform.position.x + newMove, transform.position.y);
        }
    }
}
