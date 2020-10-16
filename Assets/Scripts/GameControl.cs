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
    public GameObject Canvas;

    public float ballStartOffset;
    public float ballStartRandomRange;
    public float startSpeed;
    public float startDelay;
    public float endDelay;
    public float PlatformBoundary;
    public float PlatformBoundaryStart;
    public VideoPlayer videoPlayer;
    public Animator startAnim;
    bool Started = false;
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        for(int i = 0; i < players.Count; ++i)
        {
            if (i == GameData.AIs.Length)
            {
                for (; i < players.Count; ++i) 
                {
                    GameObject db = players[i].GetComponent<PlatformBehavior>().deathBarrier;
                    Debug.Log(db.name + "'s player is inactive, deactivating.");
                    db.GetComponent<DeathBarrierBehavior>().Destroyed = true;
                    db.GetComponent<SpriteRenderer>().material = db.GetComponent<DeathBarrierBehavior>().dead;
                    players[i].SetActive(false);
                }
                break;
            }
            if (GameData.PlayerWins.Count < players.Count) GameData.PlayerWins.Add(players[i].name, 0);
            if (GameData.AIs[i])
            {
                players[i].GetComponent<PlatformBehavior>().AI = true;
                players[i].GetComponent<BasePlatformMovement>().Speed = 0;
            }
            Vector2 pos = new Vector2(players[i].transform.position.x * ballStartOffset, players[i].transform.position.y * ballStartOffset);
            GameObject ball = Instantiate(ballObject, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            Vector2 initDir = new Vector2(players[i].transform.position.x - ball.transform.position.x, players[i].transform.position.y - ball.transform.position.y);
            if (players[i].GetComponent<PlatformBehavior>().Horizontal) initDir.x += Random.Range(-ballStartRandomRange, ballStartRandomRange);
            else initDir.y += Random.Range(-ballStartRandomRange, ballStartRandomRange);
            ball.GetComponent<Rigidbody2D>().velocity = initDir.normalized * startSpeed;
            ball.GetComponent<BallBehavior>().StartCoroutine(ball.GetComponent<BallBehavior>().Freeze(startDelay, players[i].transform));
            balls.Add(ball);
            players[i].GetComponent<BasePlatformMovement>().Boundary = PlatformBoundaryStart;
        }
        foreach(KeyValuePair<string, int> pair in GameData.PlayerWins) Debug.Log(pair.Key + ": " + pair.Value);
        StartCoroutine(LateStart());
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
    }

    public void Winner(GameObject player)
    {
        Debug.Log(player.name + " wins!");
        player.GetComponent<PlatformBehavior>().AISpeed = 0;
        player.GetComponent<BasePlatformMovement>().Speed = 0;
        GameObject db = player.GetComponent<PlatformBehavior>().deathBarrier;
        db.GetComponent<BoxCollider2D>().isTrigger = false;
        db.GetComponent<SpriteRenderer>().material = db.GetComponent<DeathBarrierBehavior>().full;
        db.GetComponent<DeathBarrierBehavior>().Destroyed = true;
        GameData.MatchCount++;
        GameObject winScreen = Instantiate(player.GetComponent<PlatformBehavior>().WinScreen, new Vector3(960, 540, -10), Quaternion.identity);
        winScreen.transform.parent = Canvas.transform;
        winScreen.GetComponent<Animator>().Play("Win", 15);
        if (++GameData.PlayerWins[player.name] >= GameData.MatchesToWin) 
        {
            GameObject winFinal = Instantiate(player.GetComponent<PlatformBehavior>().WinScreenFinal, new Vector3(960, 540, -10), Quaternion.identity);
            winFinal.transform.parent = Canvas.transform;
        } else StartCoroutine(NextRound());
        
    }
    public IEnumerator NextRound()
    {
        yield return new WaitForSeconds(endDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public IEnumerator LateStart()
    {
        yield return new WaitForSeconds(startDelay);
        foreach(GameObject player in players) player.GetComponent<BasePlatformMovement>().Boundary = PlatformBoundary;
    }
}
