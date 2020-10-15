using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int MatchesToWin;
    static public int MatchCount;
    static public Dictionary<string, int> PlayerWins;
    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerWins == null) 
        {
            PlayerWins = new Dictionary<string, int>();
        }
    }
}
