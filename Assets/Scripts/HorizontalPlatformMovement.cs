using System;
using System.Collections;
using UnityEngine;

public class HorizontalPlatformMovement : BasePlatformMovement
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
            if (transform.position.x + (Speed * Time.deltaTime) < Boundary) transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, 0);
            else transform.position = new Vector3(Boundary, transform.position.y, 0);
        }
        if(Input.GetKey(Left))
        {
            if (transform.position.x - (Speed * Time.deltaTime) > -Boundary) transform.position = new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, 0);
            else transform.position = new Vector3(-Boundary, transform.position.y, 0);
        }
    }
}
