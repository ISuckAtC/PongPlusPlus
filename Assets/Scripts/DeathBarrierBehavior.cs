using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrierBehavior : MonoBehaviour
{
    public GameObject player;

    BoxCollider2D col;
    SpriteRenderer sr;

    public Material full;
    public Material faded;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<BoxCollider2D>();
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
            GameControl gc = GameObject.Find("GameControl").GetComponent<GameControl>();
            gc.players.Remove(player);
            Destroy(player);
            if (gc.players.Count == 1)
            {
                gc.Winner(gc.players[0]);
            }
            // Forgot to push, uncomment when materials are in
            //sr.material = full;
        }
    }
}
