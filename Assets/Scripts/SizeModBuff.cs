using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeModBuff : MonoBehaviour
{
    public float SizeMod;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            collider.transform.localScale = new Vector3(SizeMod, SizeMod, 1);
            Destroy(gameObject);
        }
    }
}
