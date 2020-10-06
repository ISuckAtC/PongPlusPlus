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
            sr.material = full;
            GameControl gc = GameObject.Find("GameControl").GetComponent<GameControl>();
            if(player.GetComponent<PlatformBehavior>().Horizontal) player.GetComponent<HorizontalPlatformMovement>().Speed = 0;
            else player.GetComponent<VerticalPlatformMovement>().Speed = 0;
            player.GetComponent<BoxCollider2D>().enabled = false;
            gc.players.Remove(player);
            collider.gameObject.GetComponent<BallBehavior>().lastPlayer.GetComponent<PlatformBehavior>().playerCard.GetComponent<PlayerCardBehavior>().KillCountUpdate(1);
            collider.gameObject.GetComponent<BallBehavior>().lastPlayer.GetComponent<PlatformBehavior>().playerCard.GetComponent<PlayerCardBehavior>().BallCountUpdate(-1);
            string[] anims = player.GetComponent<PlatformBehavior>().deathAnims;
            player.GetComponent<Animator>().Play(anims[Random.Range(0, anims.Length)], 0);
            if (gc.players.Count == 1)
            {
                gc.Winner(gc.players[0]);
            }
        }
    }
}
