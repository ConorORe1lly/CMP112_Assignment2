using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool rollPhase = false;
    
    public ballRoll ballRoller;
    public GameObject ball;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    public void PlayerInput()
    {
        if (!rollPhase)
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector3.left * Time.deltaTime * 5f);
                ball.transform.Translate(Vector3.left * Time.deltaTime * 5f);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * 5f);
                ball.transform.Translate(Vector3.right * Time.deltaTime * 5f);
            }

            if (Input.GetMouseButtonDown(0))
            {
                rollPhase = true;
                ballRoller.LaunchBall();
            }
        }
    }
}
