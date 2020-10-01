using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public GameObject deathBarrier;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") 
        {
            Rigidbody2D rbo = collision.gameObject.GetComponent<Rigidbody2D>(); // rbo -> rigidbody other
            rbo.velocity = (rbo.velocity + rb.velocity).normalized * rbo.velocity.magnitude; // let platform movement alter ball trejectory
        }
    }
}
