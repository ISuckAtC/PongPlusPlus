using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BaseMenu : MonoBehaviour
{
    public EventSystem es;

    public void MenuClick()
    {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = GameData.S_ButtonSound;
    }
}
