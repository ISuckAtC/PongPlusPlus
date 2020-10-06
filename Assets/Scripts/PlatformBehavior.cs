using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public GameObject deathBarrier;
    public bool Horizontal;
    public float DI;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") 
        {
            collision.gameObject.GetComponent<SpriteRenderer>().color = sr.color;

            Vector2 diff = new Vector2(Horizontal ? collision.transform.position.x - transform.position.x : 0, Horizontal ? 0 : collision.transform.position.y - transform.position.y);
            diff *= DI;

            Rigidbody2D rbo = collision.gameObject.GetComponent<Rigidbody2D>();

            rbo.velocity = (new Vector2(rbo.velocity.x + diff.x, rbo.velocity.y + diff.y)).normalized * rbo.velocity.magnitude;
        }
    }
}
