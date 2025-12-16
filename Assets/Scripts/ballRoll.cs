using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class ballRoll : MonoBehaviour
{
    public float upperRotate;
    public float lowerRotate;
    public float launchSpeed;
    public float rotateSpeed = 1f;
    public float minLaunchSpeed = 10f;
    public float maxLaunchSpeed = 20f;

    public GameObject projection; //the red line that projects the roll angle
    public GameObject camera1; //main camera
    public GameObject ballCamera; //camera that follows the ball
    public GameObject scoreUI;
    public GameObject gameoverCanvas;

    public TextMeshProUGUI scoreRound1;
    public TextMeshProUGUI scoreRound2;
    public TextMeshProUGUI scoreRound3;
    public TextMeshProUGUI finalScore;

    public AudioSource scoreBum;

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

        //randomises the launch speed
        launchSpeed = Random.Range(minLaunchSpeed, maxLaunchSpeed);

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

        //loops through each pin to count how many are knocked down
        var pins = FindObjectsByType<pin>(FindObjectsSortMode.None);
        int knockedDown = 0;
        foreach (var pin in pins)
        {
            if (pin.KnockedOver)
                knockedDown++;
        }

        scoreBum.Play();

        //round management and score display
        //VVVVVVVVVVVVVVVVVVVVVVVVVVVVVV

        if (roundManager.Instance.currentRound <= 3)
        {
            roundManager.Instance.roundScores[roundManager.Instance.currentRound - 1] = knockedDown;
        }

        scoreRound1.text = roundManager.Instance.roundScores[0].ToString();
        scoreRound2.text = roundManager.Instance.roundScores[1].ToString();
        scoreRound3.text = roundManager.Instance.roundScores[2].ToString();

        scoreUI.SetActive(true);

        //goes to next round or ends the game if final round
        yield return new WaitForSeconds(3f);

        if (roundManager.Instance.currentRound < 3)
        {
            roundManager.Instance.currentRound++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            scoreUI.SetActive(false);
            gameoverCanvas.SetActive(true);

            //calculates score total
            int totalScore = roundManager.Instance.roundScores[0] + roundManager.Instance.roundScores[1] + roundManager.Instance.roundScores[2];
            finalScore.text = totalScore.ToString();

            //reset score for next game
            roundManager.Instance.currentRound = 1;
            for (int i = 0; i < roundManager.Instance.roundScores.Length; i++)
            {
                roundManager.Instance.roundScores[i] = 0;
            }

            yield return new WaitForSeconds(7f);
            SceneManager.LoadScene("Main menu");
        }
    }
}
