using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    public List<GameObject> players;
    public List<GameObject> balls;
    public GameObject ballObject;
    public GameObject Canvas;
    Material MatTransparent;
    Material MatDefault;

    public float ballStartOffset;
    public float ballStartRandomRange;
    public float startSpeed;
    public float SpeedUpOnKill;
    public float startDelay;
    public float endDelay;
    public float WallDisableDuration;
    public float PlatformBoundary;
    public float PlatformBoundaryStart;
    public Transform[] BallSpawns;
    public List<Flap> Flaps;
    GameObject UI;

    private Text FPSCounter;
    private bool Paused;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START");
        audioSource = GetComponent<AudioSource>();
        UI = GameObject.Find("UI");
        GameObject pauseGroup = UI.transform.GetChild(0).Find("Pause").gameObject;
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
        MatTransparent = Resources.Load<Material>("Transparent");
        MatDefault = Resources.Load<Material>("Default");
        if (!GameData.S_DisplayFPS) GameObject.Find("FPS").SetActive(false);
        else FPSCounter = GameObject.Find("FPS").GetComponent<Text>();
        if (!GetComponent<GameData>().Debug)
        {
            endDelay = GameData.S_EndDelay;
            startDelay = GameData.S_StartDelay;
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        for(int i = 0; i < players.Count; ++i)
        {
            PlatformBehavior pb = players[i].GetComponent<PlatformBehavior>();
            DeathBarrierBehavior dbb = pb.deathBarrier.GetComponent<DeathBarrierBehavior>();

            if (i == GameData.AIs.Length)
            {
                for (; i < players.Count; ++i) 
                {
                    pb = players[i].GetComponent<PlatformBehavior>();
                    dbb = pb.deathBarrier.GetComponent<DeathBarrierBehavior>();
                    Debug.Log(dbb.name + "'s player is inactive, deactivating.");
                    dbb.Destroyed = true;
                    dbb.GetComponent<SpriteRenderer>().material = dbb.dead;
                    pb.playerCard.SetActive(false);
                    players[i].SetActive(false);
                }
                break;
            }
            if (GameData.PlayerWins.Count < GameData.AIs.Count()) GameData.PlayerWins.Add(players[i].name, 0);
            if (GameData.AIs[i])
            {
                pb.AI = true;
                players[i].GetComponent<BasePlatformMovement>().Speed = 0;
            } else pb.AI = false;
            Vector2 pos = new Vector2(players[i].transform.position.x * ballStartOffset, players[i].transform.position.y * ballStartOffset);
            GameObject ball = Instantiate(ballObject, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            Vector2 initDir = new Vector2(players[i].transform.position.x - ball.transform.position.x, players[i].transform.position.y - ball.transform.position.y);
            if (pb.Horizontal) initDir.x += Random.Range(-ballStartRandomRange, ballStartRandomRange);
            else initDir.y += Random.Range(-ballStartRandomRange, ballStartRandomRange);
            ball.GetComponent<Rigidbody2D>().velocity = initDir.normalized * startSpeed;
            ball.GetComponent<BallBehavior>().StartCoroutine(ball.GetComponent<BallBehavior>().Freeze(startDelay, players[i].transform));
            balls.Add(ball);
            players[i].GetComponent<BasePlatformMovement>().Boundary = PlatformBoundaryStart;
            pb.playerCard.transform.Find("Win").GetComponent<Text>().text = "Wins: " + GameData.PlayerWins[players[i].name];

            pb.SetScore(GameData.PlayerWins[pb.name]);
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused) Time.timeScale = 1;
            else Time.timeScale = 0;
            Paused = !Paused;
        }
        if (GameData.S_DisplayFPS)
        {
            FPSCounter.text = (1f / Time.deltaTime).ToString();
        }
    }

    public void Winner(GameObject player)
    {
        Debug.Log(player.name + " wins!");
        audioSource.PlayOneShot(GameData.S_WinSound);
        player.GetComponent<PlatformBehavior>().AISpeed = 0;
        player.GetComponent<BasePlatformMovement>().Speed = 0;
        GameObject db = player.GetComponent<PlatformBehavior>().deathBarrier;
        db.GetComponent<BoxCollider2D>().isTrigger = false;
        db.GetComponent<SpriteRenderer>().material = db.GetComponent<DeathBarrierBehavior>().full;
        db.GetComponent<DeathBarrierBehavior>().Destroyed = true;
        GameData.MatchCount++;
        GameObject winScreen = Instantiate(player.GetComponent<PlatformBehavior>().WinScreen, new Vector3(960, 540, -10), Quaternion.identity);
        winScreen.transform.SetParent(Canvas.transform);
        winScreen.GetComponent<Animator>().Play("Win", 2);
        player.GetComponent<PlatformBehavior>().playerCard.transform.Find("Win").GetComponent<Text>().text = "Wins: " + ++GameData.PlayerWins[player.name];
        if (GameData.PlayerWins[player.name] >= GameData.MatchesToWin) 
        {
            GameObject winFinal = Instantiate(player.GetComponent<PlatformBehavior>().WinScreenFinal, new Vector3(960, 540, -10), Quaternion.identity);
            winFinal.transform.SetParent(Canvas.transform);
            winFinal.transform.SetAsFirstSibling();
        } else StartCoroutine(NextRound());
        
    }
    public void SpawnBall()
    {
        if (BallSpawns.Length > 0)
        {
            GameObject ball = Instantiate(ballObject, BallSpawns[Random.Range(0, BallSpawns.Length)].position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().velocity = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized * startSpeed;
            balls.Add(ball);
        }
    }
    public void SpawnBallForPlayer(GameObject player)
    {
        PlatformBehavior pb = player.GetComponent<PlatformBehavior>();
        GameObject ball = Instantiate(ballObject, pb.BoredBallSpawn.position, Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().velocity = (player.transform.position - pb.BoredBallSpawn.position).normalized * startSpeed;
        balls.Add(ball);
    }
    public void GhostWallsStart()
    {
        StopCoroutine(GhostWalls());
        StartCoroutine(GhostWalls());
    }
    public IEnumerator GhostWalls()
    {
        GameObject walls = GameObject.Find("Walls");
        if (walls == null) yield break;
        Collider2D[] colliders = walls.GetComponentsInChildren<Collider2D>(true);
        SpriteRenderer[] spriteRenderers = walls.GetComponentsInChildren<SpriteRenderer>(true);
        foreach(Collider2D c in colliders) c.enabled = false;
        foreach(SpriteRenderer sr in spriteRenderers) sr.material = MatTransparent;
        yield return new WaitForSeconds(WallDisableDuration);
        foreach(Collider2D c in colliders) c.enabled = true;
        foreach(SpriteRenderer sr in spriteRenderers) sr.material = MatDefault;
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
    public IEnumerator RespawnBuff(GameObject obj, float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        obj.SetActive(true);
        obj.GetComponent<CircleCollider2D>().enabled = true;
    }
}
