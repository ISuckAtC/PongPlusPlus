using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public bool Assign;
    public float AssignEndDelay;
    public bool Debug;
    static public string StartMap;
    public bool[] DebugAIs;
    static public bool[] AIs;
    static public int MatchesToWin;
    static public int MatchCount;
    static public Dictionary<string, int> PlayerWins;
    static public bool FriendlyFire;
    static public float EndDelay;
    // Start is called before the first frame update
    void Awake()
    {
        if (Assign)
        {
            EndDelay = AssignEndDelay;
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
