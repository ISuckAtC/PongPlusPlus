using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap : MonoBehaviour
{
    public float HowMuchRotate;
    public float MaxRotate;
    float StartRotation;
    public bool ClockWise;
    public bool taken; //If currently used by player
    KeyCode ActivaionKey;
    bool Positive;
    string Axis;
    
    void Start()
    {
        StartRotation = transform.rotation.eulerAngles.z;
        Debug.Log(StartRotation);
    }
    void Update()
    {
        float currentRotation = transform.rotation.eulerAngles.z;
        float rotation = HowMuchRotate * (ClockWise ? 1 : -1) * Time.deltaTime;
        if (Input.GetKey(ActivaionKey) || (Positive ? Input.GetAxis(Axis) > 0 : Input.GetAxis(Axis) < 0)) //cheking if activation key is pressed or if axes is  positive or negative 
        {
            if (ClockWise ? currentRotation + rotation <= MaxRotate : currentRotation - rotation >= -MaxRotate) //ckek if the rotation within the bounds
            {
                transform.Rotate(new Vector3 (0, 0, ClockWise ? rotation : -rotation)); //perfor the actual rotation
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, ClockWise ? MaxRotate : -MaxRotate);
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
}
