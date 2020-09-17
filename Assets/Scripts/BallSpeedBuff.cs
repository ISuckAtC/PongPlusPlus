using UnityEngine;

public class BallSpeedBuff : MonoBehaviour
{
    public float SpeedMod;
    public float Duration;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            BallBehavior bb = collider.GetComponent<BallBehavior>();
            bb.StartCoroutine(bb.SpeedModBuff(Duration, SpeedMod, true));
            GetComponent<CircleCollider2D>().enabled = false;
            anim.Play("SpeedUpTaken");
            //Destroy(gameObject);
        }
    }
}