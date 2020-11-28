using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    public GameObject WinScreen;
    public GameObject WinScreenFinal;
    public GameObject deathBarrier;
    public bool PostDeathControl;
    public float PostDeathControlDelay;
    public GameObject playerCard;
    public Transform BoredBallSpawn;
    public int Health;
    public bool Horizontal;
    public bool AI;
    public float DI;
    public float AISpeed;
    public float AIBoundary;
    SpriteRenderer sr;
    GameControl gc;
    public Color color;
    public string[] deathAnims;
    public float BoredCountRate;
    public int BoredToSpawn;
    private int BoredCount;
    // Start is called before the first frame update
    void Start()
    {
        BoredCount = 0;
        sr = GetComponent<SpriteRenderer>();
        gc = GameObject.Find("GameControl").GetComponent<GameControl>();
        InvokeRepeating("Bored", 0, BoredCountRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (AI) AIUpdate();
    }

    void AIUpdate()
    {
        if (Horizontal)
        {
            if (transform.position.y > 0)
            {
                List<GameObject> sortedList = gc.balls.FindAll(x => x.GetComponent<Rigidbody2D>().velocity.y > 0).OrderBy(x => x.transform.position.y).ToList();
                if (sortedList.Count == 0) return;
                GameObject closest = sortedList.Last();
                if (closest.transform.position.x > transform.position.x)
                {
                    if (transform.position.x + (AISpeed * Time.deltaTime) < AIBoundary) transform.position = new Vector3(transform.position.x + (AISpeed * Time.deltaTime), transform.position.y, 0);
                    else transform.position = new Vector3(AIBoundary, transform.position.y, 0);
                }
                else
                {
                    if (transform.position.x - (AISpeed * Time.deltaTime) > -AIBoundary) transform.position = new Vector3(transform.position.x - (AISpeed * Time.deltaTime), transform.position.y, 0);
                    else transform.position = new Vector3(-AIBoundary, transform.position.y, 0);
                }
            }
            else
            {
                List<GameObject> sortedList = gc.balls.FindAll(x => x.GetComponent<Rigidbody2D>().velocity.y < 0).OrderBy(x => x.transform.position.y).ToList();
                if (sortedList.Count == 0) return;
                GameObject closest = sortedList.First();
                if (closest.transform.position.x > transform.position.x)
                {
                    if (transform.position.x + (AISpeed * Time.deltaTime) < AIBoundary) transform.position = new Vector3(transform.position.x + (AISpeed * Time.deltaTime), transform.position.y, 0);
                    else transform.position = new Vector3(AIBoundary, transform.position.y, 0);
                }
                else
                {
                    if (transform.position.x - (AISpeed * Time.deltaTime) > -AIBoundary) transform.position = new Vector3(transform.position.x - (AISpeed * Time.deltaTime), transform.position.y, 0);
                    else transform.position = new Vector3(-AIBoundary, transform.position.y, 0);
                }
            }
        }
        else
        {
            if (transform.position.x > 0)
            {
                List<GameObject> sortedList = gc.balls.FindAll(x => x.GetComponent<Rigidbody2D>().velocity.x > 0).OrderBy(x => x.transform.position.x).ToList();
                if (sortedList.Count == 0) return;
                GameObject closest = sortedList.Last();
                if (closest.transform.position.y > transform.position.y)
                {
                    if (transform.position.y + (AISpeed * Time.deltaTime) < AIBoundary) transform.position = new Vector3(transform.position.x, transform.position.y + (AISpeed * Time.deltaTime), 0);
                    else transform.position = new Vector3(transform.position.x, AIBoundary, 0);
                }
                else
                {
                    if (transform.position.y - (AISpeed * Time.deltaTime) > -AIBoundary) transform.position = new Vector3(transform.position.x, transform.position.y - (AISpeed * Time.deltaTime), 0);
                    else transform.position = new Vector3(transform.position.x, -AIBoundary, 0);
                }
            }
            else
            {
                List<GameObject> sortedList = gc.balls.FindAll(x => x.GetComponent<Rigidbody2D>().velocity.x < 0).OrderBy(x => x.transform.position.x).ToList();
                if (sortedList.Count == 0) return;
                GameObject closest = sortedList.First();
                if (closest.transform.position.y > transform.position.y)
                {
                    if (transform.position.y + (AISpeed * Time.deltaTime) < AIBoundary) transform.position = new Vector3(transform.position.x, transform.position.y + (AISpeed * Time.deltaTime), 0);
                    else transform.position = new Vector3(transform.position.x, AIBoundary, 0);
                }
                else
                {
                    if (transform.position.y - (AISpeed * Time.deltaTime) > -AIBoundary) transform.position = new Vector3(transform.position.x, transform.position.y - (AISpeed * Time.deltaTime), 0);
                    else transform.position = new Vector3(transform.position.x, -AIBoundary, 0);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            BoredCount = 0;
            BallBehavior ball = collision.gameObject.GetComponent<BallBehavior>();
            collision.gameObject.GetComponent<SpriteRenderer>().color = color;
            if (ball.lastPlayer != null) ball.lastPlayer.GetComponent<PlatformBehavior>().playerCard.GetComponent<PlayerCardBehavior>().BallCountUpdate(-1);
            playerCard.GetComponent<PlayerCardBehavior>().BallCountUpdate(1);
            ball.lastPlayer = gameObject;

            Vector2 diff = new Vector2(Horizontal ? collision.transform.position.x - transform.position.x : 0, Horizontal ? 0 : collision.transform.position.y - transform.position.y);
            diff *= DI;

            Rigidbody2D rbo = collision.gameObject.GetComponent<Rigidbody2D>();

            rbo.velocity = (new Vector2(rbo.velocity.x + diff.x, rbo.velocity.y + diff.y)).normalized * rbo.velocity.magnitude;
        }
    }

    public IEnumerator SizeBuff(float sizeMod, float duration)
    {
        Vector3 oldScale = transform.localScale;
        transform.localScale = new Vector3(transform.localScale.x * sizeMod, transform.localScale.y, transform.localScale.z);
        yield return new WaitForSeconds(duration);
        transform.localScale = oldScale;
    }

    public void Bored()
    {
        if (++BoredCount >= BoredToSpawn)
        {
            BoredCount = 0;
            if (GameData.S_SpawnBallsOnBored) GameObject.Find("GameControl").GetComponent<GameControl>().SpawnBallForPlayer(gameObject);
            if (GameData.S_GhostWallsOnBored) GameObject.Find("GameControl").GetComponent<GameControl>().GhostWallsStart();
        }
    }
}
