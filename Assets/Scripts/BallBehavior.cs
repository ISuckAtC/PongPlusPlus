using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Vector2 InitialVelocity;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = InitialVelocity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) Debug.Log(name + " | " + rb.velocity);
    }

    public IEnumerator SizeModBuff(float duration, float mod)
    {
        transform.localScale = new Vector3(transform.localScale.x * mod, transform.localScale.y * mod, transform.localScale.z);
        yield return new WaitForSeconds(duration);
        transform.localScale = new Vector3(transform.localScale.x / mod, transform.localScale.y / mod, transform.localScale.z);
    }

    public IEnumerator SpeedModBuff(float duration, float mod)
    {
        rb.velocity *= mod;
        yield return new WaitForSeconds(duration);
        rb.velocity /= mod;
    }
}
