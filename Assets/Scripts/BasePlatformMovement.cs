using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlatformMovement : MonoBehaviour
{
    public float Speed;
    public float Boundary;
    public event EventHandler FirePositive;
    public event EventHandler FireNegative;
    
    public virtual void OnFirePositive(EventArgs e)
    {
        FirePositive.Invoke(this, e);
    }
    public virtual void OnFireNegative(EventArgs e)
    {
        FireNegative.Invoke(this, e);
    }
}
