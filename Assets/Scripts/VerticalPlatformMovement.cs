using System;
using System.Collections;
using UnityEngine;

public class VerticalPlatformMovement : MonoBehaviour
{
    // Public keycode properties allows you to set the keys in editor for easy testing
    public KeyCode Up, Down;
    public float Speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKey(Up)) 
        {
            rb.velocity = new Vector2(0, Speed);
        }
        else if(Input.GetKey(Down))
        {
            rb.velocity = new Vector2(0, -Speed);
        } else rb.velocity = Vector2.zero;
    }
}
