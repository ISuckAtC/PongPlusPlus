using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public EventSystem es;
    public string PlayScene;
    public GameObject SettingsInSelected;
    public GameObject SettingsOutSelected;
    public GameObject InfoInSelected;
    public GameObject InfoOutSelected;
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
    public void EnterSettings()
    {
        es.SetSelectedGameObject(SettingsInSelected);
    }
    public void ExitSettings()
    {
        es.SetSelectedGameObject(SettingsOutSelected);
    }
    public void EnterInfo()
    {
        es.SetSelectedGameObject(InfoInSelected);
    }
    public void ExitInfo()
    {
        es.SetSelectedGameObject(InfoOutSelected);
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
