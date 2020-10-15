using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public List<GameObject> players;
    public List<GameObject> balls;
    public GameObject ballObject;

    public float ballStartOffset;
    public float ballStartRandomRange;
    public float startSpeed;
    public float startDelay;
    public float PlatformBoundary;
    public float PlatformBoundaryStart;
    public VideoPlayer videoPlayer;
    public Animator startAnim;
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        foreach(GameObject player in players)
        {
            Vector2 pos = new Vector2(player.transform.position.x * ballStartOffset, player.transform.position.y * ballStartOffset);
            GameObject ball = Instantiate(ballObject, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            Vector2 initDir = new Vector2(player.transform.position.x - ball.transform.position.x, player.transform.position.y - ball.transform.position.y);
            if (player.GetComponent<PlatformBehavior>().Horizontal) initDir.x += Random.Range(-ballStartRandomRange, ballStartRandomRange);
            else initDir.y += Random.Range(-ballStartRandomRange, ballStartRandomRange);
            ball.GetComponent<Rigidbody2D>().velocity = initDir.normalized * startSpeed;
            ball.GetComponent<BallBehavior>().StartCoroutine(ball.GetComponent<BallBehavior>().Freeze(startDelay, player.transform));
            balls.Add(ball);
            player.GetComponent<BasePlatformMovement>().Boundary = PlatformBoundaryStart;
        }
        StartCoroutine(LateStart());
        startAnim.Play("Start3");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            List<GameObject> a = GameObject.FindGameObjectsWithTag("Wall").ToList();
            a.AddRange(GameObject.FindGameObjectsWithTag("Buff"));
            a.AddRange(GameObject.FindGameObjectsWithTag("Obstacle"));
            a.AddRange(GameObject.FindGameObjectsWithTag("Platform"));
            a.AddRange(GameObject.FindGameObjectsWithTag("Ball"));
            foreach(GameObject r in a) 
            {
                r.AddComponent<Rigidbody2D>().gravityScale = 1;
                r.AddComponent<BoxCollider2D>();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Prototype 2 Henrik", LoadSceneMode.Single);
    }

    public void Winner(GameObject player)
    {
        Debug.Log(player.name + " wins!");
        GameObject db = player.GetComponent<PlatformBehavior>().deathBarrier;
        db.GetComponent<BoxCollider2D>().isTrigger = false;
        db.GetComponent<SpriteRenderer>().material = db.GetComponent<DeathBarrierBehavior>().full;
        player.GetComponent<DeathBarrierBehavior>().Destroyed = true;
        //videoPlayer.Play();
    }
    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(startDelay);
        foreach(GameObject player in players) player.GetComponent<BasePlatformMovement>().Boundary = PlatformBoundary;
    }
}
