﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRampBehavior : MonoBehaviour
{
    public float SpeedUp;
    BoxCollider2D bc;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        bc.size = new Vector2(bc.size.x, sr.size.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            float rad = transform.eulerAngles.z * Mathf.PI / 180;
            Vector2 dir = new Vector2(-Mathf.Sin(rad), Mathf.Cos(rad)).normalized;
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(rb.velocity.x + (dir.x * SpeedUp), rb.velocity.y + (dir.y * SpeedUp));
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball") collider.GetComponent<BallBehavior>().IgnoreLimits = true;
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Ball") collider.GetComponent<BallBehavior>().IgnoreLimits = false;
    }
}
