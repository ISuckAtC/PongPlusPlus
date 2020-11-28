using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap : MonoBehaviour
{
    public float HowMuchRotate;
    public float RotateAmount;
    float StartRotation;
    float MaxRotation;
    public bool ClockWise;
    public bool taken; //If currently used by player
    KeyCode ActivaionKey;
    bool Positive;
    string Axis;
    bool GoesOverFull;
    
    void Start()
    {
        StartRotation = transform.rotation.eulerAngles.z;
        if (StartRotation + RotateAmount > 360)
        {
            GoesOverFull = true;
            MaxRotation = StartRotation + RotateAmount - 360;
        }
        else
        {
            MaxRotation = StartRotation + RotateAmount;
        }
        Debug.Log(StartRotation);
    }
    void Update()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        float rotation = HowMuchRotate * (ClockWise ? 1 : -1) * Time.deltaTime;
        if (Input.GetKey(ActivaionKey) || (Positive ? Input.GetAxis(Axis) > 0 : Input.GetAxis(Axis) < 0)) //cheking if activation key is pressed or if axes is  positive or negative 
        {
            if (ClockWise ? currentRotation + rotation <= MaxRotation : currentRotation - rotation >= -MaxRotation) //ckek if the rotation within the bounds
            {
                transform.Rotate(new Vector3 (0, 0, ClockWise ? rotation : -rotation)); //perfor the actual rotation
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, ClockWise ? MaxRotation : -MaxRotation);
            }
        } 
        else if (ClockWise ? currentRotation - rotation >= StartRotation : currentRotation + rotation <= StartRotation) //chek if within the bounds
        {
            transform.Rotate (new Vector3 (0, 0, ClockWise ? -rotation : rotation)); //perform the actual rotation
        }
        else 
        {
            transform.rotation = Quaternion.Euler(0, 0, StartRotation);
        }
    }
    public void AssignPlayer(BasePlatformMovement platmove, bool positive)
    {
        Positive = positive;
        Axis = platmove.Axis;
        ActivaionKey = positive ? platmove.Positive : platmove.Negative;
    }

    public float GetRealMin(float min) {return GoesOverFull ? min - 360 : min;}
}
