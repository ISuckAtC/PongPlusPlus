using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathBarrierBehavior : MonoBehaviour
{
    public GameObject player;

    BoxCollider2D col;
    SpriteRenderer sr;
    public bool Destroyed;

    public Material full;
    public Material faded;
    public Material dead;
    public Sprite[] Cracks;
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
        if (!GameData.FriendlyFire && collision.gameObject.GetComponent<BallBehavior>().lastPlayer == player) return;
        if (Destroyed) return;
        PlatformBehavior pb = player.GetComponent<PlatformBehavior>();
        if (--pb.Health > 0) 
        {
            if (pb.Health < Cracks.Length) sr.sprite = Cracks[Cracks.Length - pb.Health];
            return;
        }
        sr.sprite = Cracks[0];
        sr.material = faded;
        col.isTrigger = true;
        Destroyed = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Ball")
        {
            GameControl gc = GameObject.Find("GameControl").GetComponent<GameControl>();
            if (!GameData.FriendlyFire && collider.gameObject.GetComponent<BallBehavior>().lastPlayer == player)
            {
                gc.SpawnBall();
                return;
            }
            Debug.Log("Killing player " + player.name);
            col.isTrigger = false;
            collider.gameObject.layer = 8;
            sr.material = dead;
            if(player.GetComponent<PlatformBehavior>().Horizontal) player.GetComponent<HorizontalPlatformMovement>().Speed = 0;
            else player.GetComponent<VerticalPlatformMovement>().Speed = 0;
            player.GetComponent<BoxCollider2D>().enabled = false;
            gc.players.Remove(player);
            BallBehavior bb = collider.gameObject.GetComponent<BallBehavior>();
            if (!bb.lastPlayer)
            {
                PlatformBehavior pb = bb.lastPlayer.GetComponent<PlatformBehavior>();
                PlayerCardBehavior pcb = pb.playerCard.GetComponent<PlayerCardBehavior>();
                pcb.KillCountUpdate(1);
                pcb.BallCountUpdate(-1);
            }
            player.GetComponent<PlatformBehavior>().CancelInvoke("Bored");
            string[] anims = player.GetComponent<PlatformBehavior>().deathAnims;
            player.GetComponent<Animator>().Play(anims[Random.Range(0, anims.Length)], 0);
            player.GetComponent<AudioSource>().Play();
            foreach(GameObject ball in gc.balls) ball.GetComponent<Rigidbody2D>().velocity *= gc.SpeedUpOnKill;
            if (gc.players.Where(x => x.activeSelf).Count() == 1)
            {
                gc.Winner(gc.players[0]);
            }
        }
    }
}
