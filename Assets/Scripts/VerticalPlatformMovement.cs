using System;
using System.Collections;
using UnityEngine;

public class VerticalPlatformMovement : BasePlatformMovement
{
    // Public keycode properties allows you to set the keys in editor for easy testing
    public KeyCode Left, Right;

    void Start()
    {

    }

    void Update()
    {
        if (GetComponent<PlatformBehavior>().AI) return;
        if (Input.GetKey(Right)) 
        {
            if (transform.position.y - Speed > -Boundary) transform.position = new Vector3(transform.position.x, transform.position.y - Speed, 0);
            else transform.position = new Vector3(transform.position.x, -Boundary, 0);
        }
        if(Input.GetKey(Left))
        {
            if (transform.position.y + Speed < Boundary) transform.position = new Vector3(transform.position.x, transform.position.y + Speed, 0);
            else transform.position = new Vector3(transform.position.x, Boundary, 0);
        }
    }
}