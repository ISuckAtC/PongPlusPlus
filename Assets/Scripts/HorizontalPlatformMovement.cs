using System;
using System.Collections;
using UnityEngine;

public class HorizontalPlatformMovement : BasePlatformMovement
{

    void Start()
    {
        Axis = "Joy" + playerNumber + "Horizontal";
    }

    void Update()
    {
        if (GetComponent<PlatformBehavior>().AI) return;
        float joyAxis = Input.GetAxis(Axis);
        if (joyAxis != 0) 
        {
            if (transform.position.x + (Speed * joyAxis * Time.deltaTime) > -Boundary && transform.position.x + (Speed * joyAxis * Time.deltaTime) < Boundary)
                transform.position = new Vector3(transform.position.x + (Speed * joyAxis * Time.deltaTime), transform.position.y, 0);
            else transform.position = new Vector3(joyAxis * Boundary, transform.position.y, 0);
        }
        if (Input.GetKey(Positive)) 
        {
            if (transform.position.x + (Speed * Time.deltaTime) < Boundary) transform.position = new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, 0);
            else transform.position = new Vector3(Boundary, transform.position.y, 0);
        }
        if(Input.GetKey(Negative))
        {
            if (transform.position.x - (Speed * Time.deltaTime) > -Boundary) transform.position = new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, 0);
            else transform.position = new Vector3(-Boundary, transform.position.y, 0);
        }
    }
}
