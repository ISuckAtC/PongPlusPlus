using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSizeBuff : MonoBehaviour
{
    public float SizeMod;
    public float Duration;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            BallBehavior bb = collider.GetComponent<BallBehavior>();
            bb.StartCoroutine(bb.SizeModBuff(Duration, SizeMod));
            Destroy(gameObject);
        }
    }
}
