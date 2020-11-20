using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // ASSIGNABLE VALUES
    public bool Assign;
    public float AssignStartDelay;
    public float AssignEndDelay;
    public bool AssignSpawnBallsOnBored;
    public bool AssignGhostWallsOnBored;
    public bool AssignDisplayFPS;
    public string AssignMainMenuScene;
    public string AssignMapMenuScene;
    public string AssignPlayerMenuScene;



    public bool Debug;
    static public string StartMap;
    public bool[] DebugAIs;
    static public bool[] AIs;
    static public int MatchesToWin;
    static public int MatchCount;
    static public Dictionary<string, int> PlayerWins;
    static public bool FriendlyFire;
    static public float StartDelay;
    static public float EndDelay;
    static public bool SpawnBallsOnBored;
    static public bool GhostWallsOnBored;
    static public bool DisplayFPS;
    static public string MainMenuScene;
    static public string MapMenuScene;
    static public string PlayerMenuScene;
    // Start is called before the first frame update
    void Awake()
    {
        if (Assign)
        {
            StartDelay = AssignStartDelay;
            EndDelay = AssignEndDelay;
            SpawnBallsOnBored = AssignSpawnBallsOnBored;
            GhostWallsOnBored = AssignGhostWallsOnBored;
            DisplayFPS = AssignDisplayFPS;
            MainMenuScene = AssignMainMenuScene;
            MapMenuScene = AssignMapMenuScene;
            PlayerMenuScene = AssignPlayerMenuScene;
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
