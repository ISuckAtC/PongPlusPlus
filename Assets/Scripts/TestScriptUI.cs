using UnityEngine;

public class TestScriptUI : MonoBehaviour
{
    public void OnButtonPress()
    {
        Debug.Log("I got clicked");
        GameObject ball = Instantiate(Ball, new Vector3(0f,0f,0f), Quaternion.identity);
        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-3f,3f), Random.Range(-3f,3f));
    }

    public GameObject Ball;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
