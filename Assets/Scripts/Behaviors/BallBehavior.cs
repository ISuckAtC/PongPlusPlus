using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Vector2 InitialVelocity;
    public Vector2 CurrentVelocity;
    public float CurrentSpeed;
    public float SpeedLimit;
    public float SlowLimit;
    public bool IgnoreLimits;
    private Rigidbody2D rb;
    public int currentBuffs;
    public GameObject lastPlayer;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
        if (transform.position.x > 6 || transform.position.x < -6 || transform.position.y > 6 || transform.position.y < -6) 
        {
            GameObject.Find("GameControl").GetComponent<GameControl>().balls.Remove(gameObject);
            
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > SpeedLimit && !IgnoreLimits) rb.velocity = rb.velocity.normalized * SpeedLimit;
        if (rb.velocity.magnitude < SlowLimit && !IgnoreLimits) rb.velocity = rb.velocity.normalized * SlowLimit;
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

    public IEnumerator Ghost(float duration, int layerIndex, int layerNext)
    {
        gameObject.layer = layerIndex;
        yield return new WaitForSeconds(duration);
        gameObject.layer = layerNext;
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        audioSource.Play();
    }
}
