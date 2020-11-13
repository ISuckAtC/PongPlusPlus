using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierUpBuff : MonoBehaviour
{
    public int ShieldAmount;
    public string Anim;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            PlatformBehavior pb = collider.GetComponent<BallBehavior>().lastPlayer.GetComponent<PlatformBehavior>();
            pb.Health += ShieldAmount;
            GetComponent<BoxCollider2D>().enabled = false;
            anim.Play(Anim);
            //Destroy(gameObject);
        }
    }
}
