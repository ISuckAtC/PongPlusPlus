using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : BaseMenu
{
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
        SceneManager.LoadScene(GameData.S_MapMenuScene, LoadSceneMode.Single);
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
}
