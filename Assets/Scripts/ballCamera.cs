using UnityEngine;

public class ballCamera : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10); //how far camera is from ball
    public float speed = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position1 = target.position + offset;
        Vector3 position2 = Vector3.Lerp(transform.position, position1, speed * Time.deltaTime);
        transform.position = position2;

        transform.LookAt(target);
    }
}
