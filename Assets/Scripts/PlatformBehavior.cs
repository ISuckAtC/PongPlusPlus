using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball") 
        {
            Rigidbody2D rbo = collider.GetComponent<Rigidbody2D>(); // rbo -> rigidbody other
            rbo.velocity = -rbo.velocity; // bounce
            rbo.velocity = (rbo.velocity + rb.velocity).normalized * rbo.velocity.magnitude; // let platform movement alter ball trejectory
        }
    }
}
