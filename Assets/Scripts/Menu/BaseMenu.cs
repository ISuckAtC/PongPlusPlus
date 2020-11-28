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
        audio.PlayOneShot(GameData.S_ButtonSound);
    }
}
