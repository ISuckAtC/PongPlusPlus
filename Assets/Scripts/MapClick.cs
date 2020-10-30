﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MapClick : MonoBehaviour
{
    public void Click()
    {
        GameData.StartMap = gameObject.name;
        SceneManager.LoadScene("Menu_PlayerSelection", LoadSceneMode.Single);
    }
}