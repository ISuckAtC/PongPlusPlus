using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperBehavior : MonoBehaviour
{
    public float BumpPower;
    public float SpeedUp;
    public float SpeedUpDuration;

    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball") 
        {
            GameObject ball = collision.gameObject;
            Rigidbody2D rbo = ball.GetComponent<Rigidbody2D>(); // rbo -> rigidbody other

            // Calculate bump vector by finding the difference in position and multiplying the normalized vector with the modifier
            Vector2 bump = (new Vector2(rbo.transform.position.x - transform.position.x, rbo.transform.position.y - transform.position.y)).normalized * BumpPower;
            ball.GetComponent<BallBehavior>().StartCoroutine(ball.GetComponent<BallBehavior>().SpeedModBuff(SpeedUpDuration, SpeedUp, false));
            anim.Play("Bumper");
        }
    }
}
