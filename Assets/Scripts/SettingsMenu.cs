using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Text FriendlyFireText;
    // Volume settings
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void FriendlyFire()
    {
        if (GameData.FriendlyFire)
        {
            GameData.FriendlyFire = false;
            FriendlyFireText.text = "Friendly Fire Off";
        }
        else
        {
            GameData.FriendlyFire = true;
            FriendlyFireText.text = "Friendly Fire On";
        }
    }
}
