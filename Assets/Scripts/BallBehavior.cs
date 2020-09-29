using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Vector2 InitialVelocity;
    public Vector2 CurrentVelocity;
    private Rigidbody2D rb;
    public int currentBuffs;
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = InitialVelocity;
        currentBuffs = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) Debug.Log(name + " | " + rb.velocity);
        CurrentVelocity = rb.velocity;
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
        rb.velocity.normalized * (new Vector2(rb.velocity.x + (rb.velocity.x > 0 ? mod : -mod), rb.velocity.y + (rb.velocity.y > 0 ? mod : -mod))).magnitude;
        yield return new WaitForSeconds(duration);
        rb.velocity = mult ? rb.velocity / mod :
        rb.velocity.normalized * (new Vector2(rb.velocity.x - (rb.velocity.x > 0 ? mod : -mod), rb.velocity.y - (rb.velocity.y > 0 ? mod : -mod))).magnitude;
        currentBuffs--;
    }

    public IEnumerator Freeze(float duration)
    {
        Vector2 v = rb.velocity;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(duration);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.velocity = v;
    }
}
