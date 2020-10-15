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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
