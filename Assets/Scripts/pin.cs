using UnityEngine;

public class pin : MonoBehaviour
{
    public bool KnockedOver
    {
        get
        {
            //if pin is more than 30 degrees from upright it's knocked over (pin could hypothetically do a cool frontflip and be counted as knocked over and land upright again but we move anyway)
            float angle = Vector3.Angle(transform.up, Vector3.up);
            return angle > 30f;
        }
    }
}
