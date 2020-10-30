using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string PlayScene;
    //Exit button should exit game
    public void ExitGame()
    {
        Application.Quit(0);
    }
    //Switches scene from main menu to player select menu
    public void PlayMenu()
    {
        SceneManager.LoadScene(PlayScene, LoadSceneMode.Single);
    }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
