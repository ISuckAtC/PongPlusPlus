using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [HideInInspector]
    public Sprite DefaultSprite;
    public bool Respawn;
    public float RespawnTime;
    
    AudioSource audioSource;

    public virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = GameData.S_MainMixer;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball") audioSource.PlayOneShot(GameData.S_BuffSound);
    }
}