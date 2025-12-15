using UnityEngine;

public class ballRoll : MonoBehaviour
{
    public float upperRotate;
    public float lowerRotate;
    public float launchSpeed;
    public float rotateSpeed = 1f;

    public GameObject projection; //the red line that projects the roll angle
    public GameObject camera1; //main camera
    public GameObject ballCamera; //camera that follows the ball

    public PlayerMovement playerMovement;

    // Update is called once per frame
    void Update()
    {
        if (!playerMovement.rollPhase)
        {
            //rotates the projection line left and right whilst the ball hasnt been rolled yet
            float angle = Mathf.Lerp(lowerRotate, upperRotate, Mathf.PingPong(Time.time * rotateSpeed, 1f));
            if (projection != null)
            {
                projection.transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        }
    }

    public void LaunchBall()
    {
        projection.SetActive(false);

        //launch the ball in the direction of the projection line
        Rigidbody rb = GetComponent<Rigidbody>();
        Vector3 launchDirection = projection.transform.forward;
        rb.AddForce(launchDirection * launchSpeed);

        //switch cameras
        camera1.SetActive(false);
        ballCamera.SetActive(true);
    }
}
