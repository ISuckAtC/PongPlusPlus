using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCardMenu : MonoBehaviour
{
    public void PlayAgain()
    {
        GameData.PlayerWins = null;
        GameData.MatchCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ToMenu()
    {
        GameData.PlayerWins = null;
        GameData.MatchCount = 0;
        SceneManager.LoadScene(GameData.MainMenuScene);
    }
}
