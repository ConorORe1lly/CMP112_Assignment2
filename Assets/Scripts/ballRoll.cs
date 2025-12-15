using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ballRoll : MonoBehaviour
{
    public float upperRotate;
    public float lowerRotate;
    public float launchSpeed;
    public float rotateSpeed = 1f;

    public GameObject projection; //the red line that projects the roll angle
    public GameObject camera1; //main camera
    public GameObject ballCamera; //camera that follows the ball
    public GameObject scoreUI;
    public TextMeshProUGUI scoreText;

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
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        Vector3 launchDirection = projection.transform.forward;
        rb.AddForce(launchDirection * launchSpeed);

        //switch cameras
        camera1.SetActive(false);
        ballCamera.SetActive(true);

        StartCoroutine(ShowScore(4f));
    }

    IEnumerator ShowScore(float delay)
    {
        yield return new WaitForSeconds(delay);

        var pins = FindObjectsByType<pin>(FindObjectsSortMode.None);
        int knockedDown = 0;
        foreach (var pin in pins)
        {
            if (pin.KnockedOver)
                knockedDown++;
        }

        scoreText.text = knockedDown.ToString();
        scoreUI.SetActive(true);
    }
}
