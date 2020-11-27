using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatformMovement : MonoBehaviour
{
    public int playerNumber;
    public float Speed;
    public float Boundary;
    public KeyCode Positive, Negative;
    [HideInInspector]
    public string Axis;
}
