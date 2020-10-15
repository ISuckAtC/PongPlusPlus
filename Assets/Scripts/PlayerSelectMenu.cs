using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelectMenu : MonoBehaviour
{
    public void BackButton()
    {           //Loads another scene
        SceneManager.LoadScene("Prototype 2 Darja");
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Prototype 3");
    }

    public GameObject Player1, Player2, Player3, Player4, Player2Button, Player3Button, Player4Button, BackMap, BackMenu, BackPlayer;
    
    public void OnPlayer2Click()
    {
        Player2Button.SetActive(false);
        Player3Button.SetActive(false);
        Player4Button.SetActive(false);

        Player1.SetActive(true);
        Player2.SetActive(true);
    }

    public void OnPlayer3Click()
    {
        Player2Button.SetActive(false);
        Player3Button.SetActive(false);
        Player4Button.SetActive(false);

        Player1.SetActive(true);
        Player2.SetActive(true);
        Player3.SetActive(true);
    }

    public void OnPlayer4Click()
    {
        Player2Button.SetActive(false);
        Player3Button.SetActive(false);
        Player4Button.SetActive(false);

        Player1.SetActive(true);
        Player2.SetActive(true);
        Player3.SetActive(true);
        Player4.SetActive(true);
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
