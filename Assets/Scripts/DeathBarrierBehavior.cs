using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrierBehavior : MonoBehaviour
{
    public GameObject player;

    BoxCollider2D col;
    SpriteRenderer sr;
    [HideInInspector] public bool Destroyed;

    public Material full;
    public Material faded;
    public Material dead;
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Destroyed) return;
        if (--player.GetComponent<PlatformBehavior>().Health > 0) 
        {
            return;
        }
        sr.material = faded;
        col.isTrigger = true;
        Destroyed = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            Debug.Log("Killing player " + player.name);
            col.isTrigger = false;
            collider.gameObject.layer = 8;
            sr.material = dead;
            GameControl gc = GameObject.Find("GameControl").GetComponent<GameControl>();
            if(player.GetComponent<PlatformBehavior>().Horizontal) player.GetComponent<HorizontalPlatformMovement>().Speed = 0;
            else player.GetComponent<VerticalPlatformMovement>().Speed = 0;
            player.GetComponent<BoxCollider2D>().enabled = false;
            gc.players.Remove(player);
            collider.gameObject.GetComponent<BallBehavior>().lastPlayer.GetComponent<PlatformBehavior>().playerCard.GetComponent<PlayerCardBehavior>().KillCountUpdate(1);
            collider.gameObject.GetComponent<BallBehavior>().lastPlayer.GetComponent<PlatformBehavior>().playerCard.GetComponent<PlayerCardBehavior>().BallCountUpdate(-1);
            string[] anims = player.GetComponent<PlatformBehavior>().deathAnims;
            player.GetComponent<Animator>().Play(anims[Random.Range(0, anims.Length)], 0);
            player.GetComponent<AudioSource>().Play();
            if (gc.players.Count == 1)
            {
                gc.Winner(gc.players[0]);
            }
        }
    }
}
