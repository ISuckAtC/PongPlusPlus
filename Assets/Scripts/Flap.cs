using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap : MonoBehaviour
{
    public Transform TopPivot, BotPivot, LeftPivot, RightPivot;
    public float HowMuchRotate;
    public float MaxRotate;
    public bool ClockWise;

    public void ToMoveFlap(object sender, EventArgs e)
    {
        transform.Rotate(new Vector3 (0, 0, HowMuchRotate));
    }
    public void AssignPlayer(BasePlatformMovement platmove)
    {
        platmove.FirePositive += ToMoveFlap;

    }
}
