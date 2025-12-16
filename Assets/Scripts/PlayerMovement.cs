using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool rollPhase = false;
    
    public ballRoll ballRoller;
    public GameObject ball;
    public Rigidbody ballRigidbody;

    public float minX = -5f;
    public float maxX = 5f;

    public Animator animator;

    private float ballY;
    private float ballZ;

    public AudioSource swooshSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
        {
        ballY = ball.transform.position.y;
        ballZ = ball.transform.position.z;
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
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector3.right * Time.deltaTime * 5f);
            }

            //stops player from moving away from the lane
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

            ball.transform.position = new Vector3(clampedX, ballY, ballZ);
            ball.transform.rotation = Quaternion.Euler(0, 0, 0);

            if (Input.GetMouseButtonDown(0))
            {
                rollPhase = true;
                animator.SetTrigger("startRoll");

                //ball starts spinning like crazy if you dont constrain it, something to do with clamp
                ballRigidbody.constraints = RigidbodyConstraints.FreezeAll;

            }
        }
    }

    public void AnimFinished()
    {
        ballRoller.LaunchBall();
        ballRigidbody.constraints = RigidbodyConstraints.None;
        swooshSound.Play();
    }
}
