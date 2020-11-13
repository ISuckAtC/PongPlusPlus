using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cogwheel : MonoBehaviour
{
    public float RotateSpeed;
    void Update()
    {
        transform.Rotate(Vector3.forward, RotateSpeed);
    }
}
