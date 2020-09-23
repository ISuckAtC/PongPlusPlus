using System.Collections;
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
        bc.size = sr.size;
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
}
