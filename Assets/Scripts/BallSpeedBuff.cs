using UnityEngine;

public class BallSpeedBuff : MonoBehaviour
{
    public float SpeedMod;
    public float Duration;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            BallBehavior bb = collider.GetComponent<BallBehavior>();
            bb.StartCoroutine(bb.SpeedModBuff(Duration, SpeedMod, true));
            Destroy(gameObject);
        }
    }
}