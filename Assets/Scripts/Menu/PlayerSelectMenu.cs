using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectMenu : BaseMenu
{
    public int MatchLimit;
    public int MatchCount;
    public string MapSelectScene;

    public void PlayButton()
    {
        GameData.MatchesToWin = MatchCount;
        SceneManager.LoadScene(GameData.StartMap);
    }

    public GameObject Player1, Player2, Player3, Player4, Player2Button, Player3Button, Player4Button, BackMap, BackMenu, BackPlayer, MatchSelectButton, PlaySelectButton;
    
    public void OnPlayer2Click()
    {
        GameData.AIs = new bool[2];
        Player2Button.SetActive(false);
        Player3Button.SetActive(false);
        Player4Button.SetActive(false);

        Player1.SetActive(true);
        Player2.SetActive(true);

        BackPlayer.SetActive(true);
        BackMenu.SetActive(false);
        MatchSelectButton.SetActive(true);
        PlaySelectButton.SetActive(true);

        es.SetSelectedGameObject(Player1);
    }

    public void OnPlayer3Click()
    {
        GameData.AIs = new bool[3];
        Player2Button.SetActive(false);
        Player3Button.SetActive(false);
        Player4Button.SetActive(false);

        Player1.SetActive(true);
        Player2.SetActive(true);
        Player3.SetActive(true);

        BackPlayer.SetActive(true);
        BackMenu.SetActive(false);
        MatchSelectButton.SetActive(true);
        PlaySelectButton.SetActive(true);
        
        es.SetSelectedGameObject(Player1);
    }

    public void OnPlayer4Click()
    {
        GameData.AIs = new bool[4];
        Player2Button.SetActive(false);
        Player3Button.SetActive(false);
        Player4Button.SetActive(false);

        Player1.SetActive(true);
        Player2.SetActive(true);
        Player3.SetActive(true);
        Player4.SetActive(true);

        BackPlayer.SetActive(true);
        BackMenu.SetActive(false);
        MatchSelectButton.SetActive(true);
        PlaySelectButton.SetActive(true);

        es.SetSelectedGameObject(Player1);
    }

    public void OnBackPlayerButtonClick()
    {
        Player1.SetActive(false);
        Player2.SetActive(false);
        Player3.SetActive(false);
        Player4.SetActive(false);

        Player2Button.SetActive(true);
        Player3Button.SetActive(true);
        Player4Button.SetActive(true);

        MatchSelectButton.SetActive(false);
        BackPlayer.SetActive(false);
        BackMenu.SetActive(true);
        PlaySelectButton.SetActive(false);

        es.SetSelectedGameObject(Player2Button);
    }

    public void OnbackMenuButtonClick()
    {
        SceneManager.LoadScene(MapSelectScene);
    }

    public void SetBot(int p)
    {
        GameObject player = GameObject.Find("Players").transform.GetChild(p).gameObject;
        Debug.Log(player.name);
        if (GameData.AIs[p]) 
        {
            // Player is already AI, set sprite to normal and change to non AI
            player.GetComponentInChildren<Text>().text = "Player"; //replace null with spritename
            GameData.AIs[p] = false;
        }
        else
        {
            // Player is not AI, set sprite to AI and change to AI
            player.GetComponentInChildren<Text>().text = "Bot";
            GameData.AIs[p] = true;
        }
    }

    public void OnMatchCountClick()
    {
        if (MatchCount == MatchLimit) MatchCount = 1;
        else MatchCount++;
        MatchSelectButton.transform.GetComponentInChildren<Text>().text = "First to " + MatchCount + " wins";
    }

    // Start is called before the first frame update
    void Start()
    {
        Player1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
