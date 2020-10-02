using System;
using System.Collections;
using UnityEngine;

public class HorizontalPlatformMovement : MonoBehaviour
{
    // Public keycode properties allows you to set the keys in editor for easy testing
    public KeyCode Left, Right;
    public float Speed;
    public float Boundary;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKey(Right)) 
        {
            if (transform.position.x + Speed < Boundary) transform.position = new Vector3(transform.position.x + Speed, transform.position.y, 0);
            else transform.position = new Vector3(Boundary, transform.position.y, 0);
        }
        if(Input.GetKey(Left))
        {
            if (transform.position.x - Speed > -Boundary) transform.position = new Vector3(transform.position.x - Speed, transform.position.y, 0);
            else transform.position = new Vector3(-Boundary, transform.position.y, 0);
        }
    }
}
