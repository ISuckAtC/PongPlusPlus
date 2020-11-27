using System;
using System.Collections;
using UnityEngine;

public class VerticalPlatformMovement : BasePlatformMovement
{
    public int playerNumber;
    // Public keycode properties allows you to set the keys in editor for easy testing
    public KeyCode Left, Right;

    void Start()
    {

    }

    void Update()
    {
        if (GetComponent<PlatformBehavior>().AI) return;
        float joyAxis = Input.GetAxis("Joy" + playerNumber + "Vertical");
        if (joyAxis != 0)
        {
            if (joyAxis > 0) base.OnFirePositive(EventArgs.Empty);
            else base.OnFireNegative(EventArgs.Empty);
            if (transform.position.y + (Speed * joyAxis * Time.deltaTime) > -Boundary && transform.position.y + (Speed * joyAxis * Time.deltaTime) < Boundary)
                transform.position = new Vector3(transform.position.x, transform.position.y + (Speed * joyAxis * Time.deltaTime), 0);
            else transform.position = new Vector3(transform.position.x, joyAxis * Boundary, 0);
        }
        if (Input.GetKey(Right)) 
        {
            if (transform.position.y - (Speed * Time.deltaTime) > -Boundary) transform.position = new Vector3(transform.position.x, transform.position.y - (Speed * Time.deltaTime), 0);
            else transform.position = new Vector3(transform.position.x, -Boundary, 0);
        }
        if(Input.GetKey(Left))
        {
            if (transform.position.y + (Speed * Time.deltaTime) < Boundary) transform.position = new Vector3(transform.position.x, transform.position.y + (Speed * Time.deltaTime), 0);
            else transform.position = new Vector3(transform.position.x, Boundary, 0);
        }
    }
}