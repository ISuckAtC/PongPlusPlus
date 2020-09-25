using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCardBehavior : MonoBehaviour
{
    int[] playerBallCount;

    public void UIUpdate(int amount, int player)
    {
        if (player < 0 || player > 3) throw new ArgumentOutOfRangeException("player", player, "player must be between 0 and 3");
        playerBallCount[player] += amount;

        GameObject card = transform.GetChild(player).gameObject;

        card.transform.Find("Balls").GetComponent<Text>().text = "Balls: " + playerBallCount[player];
    }

    void Start()
    {
        playerBallCount = new int[] {0, 0, 0, 0};
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UIUpdate(1, 0);
        }
    }
}
