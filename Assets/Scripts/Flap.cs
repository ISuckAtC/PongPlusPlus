using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flap : MonoBehaviour
{
    public float RotateAmount;
    public float RotationSpeed;
    float StartRotation;
    float MaxRotation;
    float CurrentRotation;
    public bool ClockWise;
    public bool taken; //If currently used by player
    public KeyCode ActivationKey;
    bool Positive;
    public string Axis;
    
    void Start()
    {
        StartRotation = transform.rotation.eulerAngles.z;
        CurrentRotation = StartRotation;
        MaxRotation = StartRotation + (ClockWise ? RotateAmount : -RotateAmount);
    }
    void Update()
    {
        float rotation = RotationSpeed * (ClockWise ? 1 : -1) * Time.deltaTime;
        if (Input.GetKey(ActivationKey) || (Positive ? Input.GetAxis(Axis) > 0 : Input.GetAxis(Axis) < 0)) //checking if activation key is pressed or if axes is  positive or negative 
        {
            if (ClockWise ? CurrentRotation + rotation <= MaxRotation : CurrentRotation - rotation >= MaxRotation) //check if the rotation within the bounds
            {
                CurrentRotation += ClockWise ? rotation : -rotation;
            }
            else
            {
                CurrentRotation = MaxRotation;
            }
        } 
        else if (ClockWise ? CurrentRotation - rotation >= StartRotation : CurrentRotation + rotation <= StartRotation) //check if within the bounds
        {
            CurrentRotation -= ClockWise ? rotation : -rotation;
        }
        else 
        {
            CurrentRotation = StartRotation;
        }

        transform.rotation = Quaternion.Euler(0, 0, CurrentRotation); //perform the actual rotation
    }
    public void AssignPlayer(BasePlatformMovement platmove, bool positive)
    {
        Positive = positive;
        Axis = platmove.Axis;
        ActivationKey = positive ? platmove.Positive : platmove.Negative;
    }
}
