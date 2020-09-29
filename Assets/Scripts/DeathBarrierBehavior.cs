using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrierBehavior : MonoBehaviour
{
    public GameObject player;

    Collider2D col;
    SpriteRenderer sr;

    public Material full;
    public Material faded;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {

            col.isTrigger = false;
            collider.gameObject.layer = 8;
            Destroy(player);
            // Forgot to push, uncomment when materials are in
            //sr.material = full;
        }
    }
}
