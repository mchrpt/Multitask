using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocky : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject player;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(Random.Range(.05f, 0.25f), Random.Range(.05f, 0.25f), 1);
        health = Mathf.CeilToInt((transform.localScale.x + 2) * (transform.localScale.y + 1)) * 2;
        rb.AddForce(new Vector3(Random.Range(-150f, 150f), Random.Range(-150f, 150f), 0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        healthCheck();
        checkIfMoving();
    }
    void healthCheck()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    void checkIfMoving()
    {
        if(rb.velocity == Vector2.zero)
        {
            rb.AddForce(new Vector3(Random.Range(-150f, 150f), Random.Range(-150f, 150f), 0));
        }else if(Mathf.Abs(rb.velocity.x) > 7 || Mathf.Abs(rb.velocity.y) > 7)
        {
            rb.velocity = rb.velocity.normalized * 7;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Laser")
        {
            health--;
        }
    }
}
