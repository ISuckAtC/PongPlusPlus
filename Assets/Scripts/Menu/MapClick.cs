using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapClick : MonoBehaviour
{
    public string PlayerSelectScene;
    public void Click()
    {
        GameData.StartMap = gameObject.name;
        SceneManager.LoadScene(PlayerSelectScene, LoadSceneMode.Single);
    }
}
