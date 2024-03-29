﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSizeBuff : Buff
{
    public float SizeMod;
    public float Duration;
    public string Anim;
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
            PlatformBehavior pb = collider.GetComponent<BallBehavior>().lastPlayer.GetComponent<PlatformBehavior>();
            pb.StartCoroutine(pb.SizeBuff(SizeMod, Duration));
            GetComponent<BoxCollider2D>().enabled = false;
            anim.Play(Anim);
            //Destroy(gameObject);
        }
    }
}
