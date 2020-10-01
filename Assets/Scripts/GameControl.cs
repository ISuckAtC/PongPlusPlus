using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameControl : MonoBehaviour
{
    public List<GameObject> players;
    public GameObject ballObject;

    public float ballStartOffset;
    public float startSpeed;
    public float startDelay;
    public VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject player in players)
        {
            Vector2 pos = new Vector2(player.transform.position.x * ballStartOffset, player.transform.position.y * ballStartOffset);
            GameObject ball = Instantiate(ballObject, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().velocity = (new Vector2(player.transform.position.x - ball.transform.position.x, player.transform.position.y - ball.transform.position.y)).normalized * startSpeed;
            ball.GetComponent<BallBehavior>().StartCoroutine(ball.GetComponent<BallBehavior>().Freeze(startDelay));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Winner(GameObject player)
    {
        Debug.Log(player.name + " wins!");
        player.GetComponent<PlatformBehavior>().deathBarrier.GetComponent<BoxCollider2D>().isTrigger = false;
        //videoPlayer.Play();
    }
}
