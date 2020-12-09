using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathBarrierBehavior : MonoBehaviour
{
    public GameObject player;

    BoxCollider2D col;
    SpriteRenderer sr;
    AudioSource audioSource;
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
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = GameData.S_MainMixer;
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
        audioSource.PlayOneShot(GameData.S_BreakSound);
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
            PlatformBehavior platform = player.GetComponent<PlatformBehavior>();
            if (!GameData.FriendlyFire && collider.gameObject.GetComponent<BallBehavior>().lastPlayer == player)
            {
                gc.SpawnBall();
                return;
            }
            Debug.Log("Killing player " + player.name);
            audioSource.PlayOneShot(GameData.S_DeathSound);
            col.isTrigger = false;
            collider.gameObject.layer = 8;
            sr.material = dead;
            if(platform.Horizontal) player.GetComponent<HorizontalPlatformMovement>().Speed = 0;
            else player.GetComponent<VerticalPlatformMovement>().Speed = 0;
            player.GetComponent<BoxCollider2D>().enabled = false;
            gc.players.Remove(player);
            BallBehavior bb = collider.gameObject.GetComponent<BallBehavior>();
            if (bb.lastPlayer != null)
            {
                PlatformBehavior pb = bb.lastPlayer.GetComponent<PlatformBehavior>();
                PlayerCardBehavior pcb = pb.playerCard.GetComponent<PlayerCardBehavior>();
                pcb.KillCountUpdate(1);
                pcb.BallCountUpdate(-1);
            }
            platform.CancelInvoke("Bored");
            if (platform.PostDeathControl)
            {
                if (gc.Flaps.Exists(x => !x.GetComponent<Flap>().taken))
                {
                    Flap f = gc.Flaps.First(x => !x.taken);
                    f.taken = true;
                    f.AssignPlayer(player.GetComponent<BasePlatformMovement>(), true);
                }
                if (gc.Flaps.Count > 3 && gc.Flaps.Exists(x => !x.GetComponent<Flap>().taken))
                {
                    Flap f = gc.Flaps.First(x => !x.taken);
                    f.taken = true;
                    f.AssignPlayer(player.GetComponent<BasePlatformMovement>(), false);
                }
            }
            string[] anims = platform.deathAnims;
            player.GetComponent<Animator>().Play(anims[Random.Range(0, anims.Length)], 0);
            foreach(GameObject ball in gc.balls) ball.GetComponent<Rigidbody2D>().velocity *= gc.SpeedUpOnKill;
            if (gc.players.Where(x => x.activeSelf).Count() == 1)
            {
                gc.Winner(gc.players[0]);
            }
        }
    }
}
