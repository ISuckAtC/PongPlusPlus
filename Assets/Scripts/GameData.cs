using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameData : MonoBehaviour
{
    // ASSIGNABLE VALUES
    public bool Assign;
    public float StartDelay;
    public float EndDelay;
    public bool SpawnBallsOnBored;
    public bool GhostWallsOnBored;
    public bool DisplayFPS;
    public string MainMenuScene;
    public string MapMenuScene;
    public string PlayerMenuScene;
    public AudioMixerGroup MainMixer;

    public AudioClip WinSound, BuffSound, BumperSound, ButtonSound, BounceSound, DeathSound, BreakSound;
    public int AISpeed;


    public bool Debug;
    static public string StartMap;
    public bool[] DebugAIs;
    static public bool[] AIs;
    static public int MatchesToWin;
    static public int MatchCount;
    static public Dictionary<string, int> PlayerWins;
    static public bool FriendlyFire;
    static public float S_StartDelay;
    static public float S_EndDelay;
    static public bool S_SpawnBallsOnBored;
    static public bool S_GhostWallsOnBored;
    static public bool S_DisplayFPS;
    static public string S_MainMenuScene;
    static public string S_MapMenuScene;
    static public string S_PlayerMenuScene;
    static public int S_AISpeed;
    static public AudioMixerGroup S_MainMixer;

    static public AudioClip S_WinSound, S_BuffSound, S_BumperSound, S_ButtonSound, S_BounceSound, S_DeathSound, S_BreakSound;
    // Start is called before the first frame update
    void Awake()
    {
        if (Assign)
        {
            S_StartDelay = StartDelay;
            S_EndDelay = EndDelay;
            S_SpawnBallsOnBored = SpawnBallsOnBored;
            S_GhostWallsOnBored = GhostWallsOnBored;
            S_DisplayFPS = DisplayFPS;
            S_MainMenuScene = MainMenuScene;
            S_MapMenuScene = MapMenuScene;
            S_PlayerMenuScene = PlayerMenuScene;
            S_WinSound = WinSound;
            S_BuffSound = BuffSound;
            S_BumperSound = BumperSound;
            S_ButtonSound = ButtonSound;
            S_BounceSound = BounceSound;
            S_DeathSound = DeathSound;
            S_BreakSound = BreakSound;
            S_MainMixer = MainMixer;
            S_AISpeed = AISpeed;
        }
        if (Debug)
        {
            MatchesToWin = 1;
            MatchCount = 0;
            AIs = DebugAIs;
        }
        if (PlayerWins == null)
        {
            PlayerWins = new Dictionary<string, int>();
        }
    }
}
