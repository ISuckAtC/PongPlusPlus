using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullBehavior : MonoBehaviour
{
    public float pullStrength;
    public bool Horizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        GameObject o = collider.gameObject;
        Rigidbody2D rbo = o.GetComponent<Rigidbody2D>();
        if (rbo.velocity.y * (transform.position.y - collider.transform.position.y) > 0 && rbo.velocity.x * (transform.position.x - collider.transform.position.x) > 0)
        {    
            if (Horizontal)
            {
                rbo.velocity = (new Vector2(
                    rbo.velocity.x + ((transform.position.x - collider.transform.position.x) * pullStrength), 
                    rbo.velocity.y + ((transform.position.y - collider.transform.position.y) * pullStrength)))
                    .normalized * rbo.velocity.magnitude;
            }
            else
            {
                rbo.velocity = (new Vector2(
                    rbo.velocity.x + ((transform.position.x - collider.transform.position.x) * pullStrength), 
                    rbo.velocity.y + ((transform.position.y - collider.transform.position.y) * pullStrength)))
                    .normalized * rbo.velocity.magnitude;
            }
        }
    }
}
