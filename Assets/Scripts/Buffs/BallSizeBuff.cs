using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSizeBuff : Buff
{
    public float SizeMod;
    public float Duration;
    public string Animation;
    Animator anim;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        DefaultSprite = GetComponent<SpriteRenderer>().sprite;
    }

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
        if (collider.tag == "Ball")
        {
            BallBehavior bb = collider.GetComponent<BallBehavior>();
            bb.StartCoroutine(bb.SizeModBuff(Duration, SizeMod));
            GetComponent<CircleCollider2D>().enabled = false;
            anim.Play(Animation);
            //Destroy(gameObject);
        }
    }
}
