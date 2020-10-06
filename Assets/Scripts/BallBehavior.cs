﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Vector2 InitialVelocity;
    public Vector2 CurrentVelocity;
    public float CurrentSpeed;
    public float SpeedLimit;
    private Rigidbody2D rb;
    public int currentBuffs;
    public GameObject lastPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = InitialVelocity;
        lastPlayer = null;
        currentBuffs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) Debug.Log(name + " | " + rb.velocity);
        CurrentVelocity = rb.velocity;
        CurrentSpeed = rb.velocity.magnitude;
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > SpeedLimit) rb.velocity = rb.velocity.normalized * SpeedLimit;
    }

    public IEnumerator SizeModBuff(float duration, float mod)
    {
        currentBuffs++;
        transform.localScale = new Vector3(transform.localScale.x * mod, transform.localScale.y * mod, transform.localScale.z);
        yield return new WaitForSeconds(duration);
        transform.localScale = new Vector3(transform.localScale.x / mod, transform.localScale.y / mod, transform.localScale.z);
        currentBuffs--;
    }

    public IEnumerator SpeedModBuff(float duration, float mod, bool mult)
    {
        currentBuffs++;
        rb.velocity = mult ? rb.velocity * mod :
        rb.velocity.normalized * (rb.velocity.magnitude + mod);
        yield return new WaitForSeconds(duration);
        rb.velocity = mult ? rb.velocity / mod :
        rb.velocity.normalized * (rb.velocity.magnitude - mod);
        currentBuffs--;
    }

    public IEnumerator Freeze(float duration, Transform parent = null)
    {
        if (parent) transform.parent = parent;
        Vector2 v = rb.velocity;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(duration);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = v;
        if (parent) transform.parent = null;
    }
}
