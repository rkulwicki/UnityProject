using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public float maxAngle;
    public float minAngle;
    public float angle;
    public float rotationFactor;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(RotateTask());
    }

    IEnumerator RotateTask()
    {
        float timer = 0;

        while (true) // this could also be a condition indicating "alive or dead"
        {
            // we scale all axis, so they will have the same value, 
            // so we can work with a float instead of comparing vectors
            if (maxAngle < minAngle)
                maxAngle = minAngle;

            while (maxAngle > angle)
            {
                timer += Time.deltaTime;
                angle += Time.deltaTime * rotationFactor;
                transform.localRotation = Quaternion.Euler(0, 0, angle);
                yield return null;
            }
            // reset the timer

            timer = 0;
            while (minAngle < angle)
            {
                timer += Time.deltaTime;
                angle -= Time.deltaTime * rotationFactor;
                transform.localRotation = Quaternion.Euler(0, 0, angle);
                yield return null;
            }

            timer = 0;
        }
    }
}
