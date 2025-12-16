using System.Threading;
using UnityEngine;

public class scoreSound : MonoBehaviour
{
    public AudioSource lowBum;
    public AudioSource highBum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //these functions are triggered by animation events
    public void firstSound()
    {
        lowBum.Play();
    }

    public void secondSound()
    {
        highBum.Play();
    }
}
