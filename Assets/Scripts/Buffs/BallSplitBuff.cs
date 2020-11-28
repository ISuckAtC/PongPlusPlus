using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BallSplitBuff : Buff
{
    public float SplitAngle;
    public float SplitNum;
    public float Degrees;
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
            gameObject.GetComponent<Collider2D>().enabled = false;
            BallBehavior bb = collider.GetComponent<BallBehavior>();
            bb.StartCoroutine(bb.Ghost(0.5f, 12, 11));
            for(int i = 1; i > -((SplitNum + 1) / 2) && i - 1 < (SplitNum / 2); i = i > 0 ? i * -1 : (i * -1) + 1)
            {
                GameObject b = Instantiate(collider.gameObject, collider.transform.position, collider.transform.localRotation);
                BallBehavior bbcopy = b.GetComponent<BallBehavior>();
                if (bb.lastPlayer != null)
                {
                    bbcopy.lastPlayer = bb.lastPlayer;
                    bbcopy.lastPlayer.GetComponent<PlatformBehavior>().playerCard.GetComponent<PlayerCardBehavior>().KillCountUpdate(1);
                } else bbcopy.lastPlayer = null;
                bbcopy.StartCoroutine(b.GetComponent<BallBehavior>().Ghost(0.5f, 12, 11));
                b.GetComponent<Rigidbody2D>().velocity = RotateVector(b.GetComponent<Rigidbody2D>().velocity, (Degrees * -i) * (Mathf.PI / 180));
                GameObject.Find("GameControl").GetComponent<GameControl>().balls.Add(b);
            }
            anim.Play("SplitTaken");
            //Destroy(gameObject);
        }
    }

    Vector2 RotateVector(Vector2 o, float radians)
    {
        float x = (Mathf.Cos(radians) * o.x) - (Mathf.Sin(radians) * o.y);
        float y = (Mathf.Sin(radians) * o.x) + (Mathf.Cos(radians) * o.y);
        return new Vector2(x, y);
    }
}
