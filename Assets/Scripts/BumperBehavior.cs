using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperBehavior : MonoBehaviour
{
    public float BumpPower;
    // Start is called before the first frame update
    void Start()
    {
        
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

            // Calculate bump vector by finding the difference in position and multiplying the normalized vector with the modifier
            Vector2 bump = (new Vector2(rbo.transform.position.x - transform.position.x, rbo.transform.position.y - transform.position.y)).normalized * BumpPower;
            rbo.velocity += bump;
        }
    }
}
