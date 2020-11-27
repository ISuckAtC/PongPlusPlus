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
    public bool taken; //If currently used by player

    public void ToMoveFlap(object sender, EventArgs e)
    {
        transform.Rotate(new Vector3 (0, 0, HowMuchRotate));
    }
    public void AssignPlayer(BasePlatformMovement platmove, bool positive)
    {
        if (positive)
        {
            platmove.FirePositive += ToMoveFlap;
        }
        else
        {
            platmove.FireNegative += ToMoveFlap;
        }
    }
}
