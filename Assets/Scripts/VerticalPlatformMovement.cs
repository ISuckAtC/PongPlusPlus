using System;
using System.Collections;
using UnityEngine;

public class VerticalPlatformMovement : BasePlatformMovement
{

    void Start()
    {
        Axis = "Joy" + playerNumber + "Vertical";
    }

    void Update()
    {
        if (GetComponent<PlatformBehavior>().AI) return;
        float joyAxis = Input.GetAxis(Axis);
        if (joyAxis != 0)
        {
            if (transform.position.y + (Speed * joyAxis * Time.deltaTime) > -Boundary && transform.position.y + (Speed * joyAxis * Time.deltaTime) < Boundary)
                transform.position = new Vector3(transform.position.x, transform.position.y + (Speed * joyAxis * Time.deltaTime), 0);
            else transform.position = new Vector3(transform.position.x, joyAxis * Boundary, 0);
        }
        if (Input.GetKey(Positive)) 
        {
            if (transform.position.y - (Speed * Time.deltaTime) > -Boundary) transform.position = new Vector3(transform.position.x, transform.position.y - (Speed * Time.deltaTime), 0);
            else transform.position = new Vector3(transform.position.x, -Boundary, 0);
        }
        if(Input.GetKey(Negative))
        {
            if (transform.position.y + (Speed * Time.deltaTime) < Boundary) transform.position = new Vector3(transform.position.x, transform.position.y + (Speed * Time.deltaTime), 0);
            else transform.position = new Vector3(transform.position.x, Boundary, 0);
        }
    }
}