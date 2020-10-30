using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardBehavior : MonoBehaviour
{
    int ballCount;
    int killCount;
    int WinCount;
    public void BallCountUpdate(int amount)
    {
        ballCount += amount;

        transform.Find("Balls").GetComponent<Text>().text = "Balls: " + ballCount;
    }

    public void KillCountUpdate(int amount)
    {
        killCount += amount;

        transform.Find("Kills").GetComponent<Text>().text = "Kills:" + killCount;
    }

    public void WinCountUpdate(int amount)
    {
        WinCount += amount;

        transform.Find("Score").GetComponent<Text>().text = "Score:" + WinCount;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.U))
        {
            BallCountUpdate(1);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            KillCountUpdate(1);
        }*/
    }
}
