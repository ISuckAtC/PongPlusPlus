using System;
using System.Collections;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    // Public keycode properties allows you to set the keys in editor for easy testing
    public KeyCode Up, Down;
    public float speed;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKey(Up)) 
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        
        if(Input.GetKey(Down))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
    }
}
