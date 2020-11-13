﻿using UnityEngine;

public class BallSpeedBuff : Buff
{
    public float SpeedMod;
    public float Duration;
    public string Anim;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        DefaultSprite = GetComponent<SpriteRenderer>().sprite;
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            BallBehavior bb = collider.GetComponent<BallBehavior>();
            bb.StartCoroutine(bb.SpeedModBuff(Duration, SpeedMod, true));
            GetComponent<CircleCollider2D>().enabled = false;
            anim.Play(Anim);
            //Destroy(gameObject);
        }
    }
}