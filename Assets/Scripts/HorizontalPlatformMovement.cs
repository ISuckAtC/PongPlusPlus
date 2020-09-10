using System;
using System.Collections;
using UnityEngine;

public class HorizontalPlatformMovement : MonoBehaviour
{
    // Public keycode properties allows you to set the keys in editor for easy testing
    public KeyCode Left, Right;
    public float speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKey(Right)) 
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else if(Input.GetKey(Left))
        {
            rb.velocity = new Vector2(-speed, 0);
        } else rb.velocity = Vector2.zero;
    }
}
