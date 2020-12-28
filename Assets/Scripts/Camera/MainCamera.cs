using UnityEngine;
using System.Collections;
using static Globals;
public class MainCamera : MonoBehaviour
{

    // the target the camera should follow (usually the player)
    public Transform target;

    // the camera distance (z position)
    public float distance = -10f;

    // the height the camera should be above the target (AKA player)
    public float height = 0f;

    // damping is the amount of time the camera should take to go to the target
    public float damping = 5.0f;

    // just private var for the map boundaries
    public float minX = 0f;
    public float maxX = 0f;
    public float minY = 0f;
    public float maxY = 0f;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(PlayerTag).transform;
    }

    void Update()
    {

        // get the position of the target (AKA player)
        Vector3 wantedPosition = target.TransformPoint(0, height, distance);

        // check if it's inside the boundaries on the X position
        wantedPosition.x = (wantedPosition.x < minX) ? minX : wantedPosition.x;
        wantedPosition.x = (wantedPosition.x > maxX) ? maxX : wantedPosition.x;

        // check if it's inside the boundaries on the Y position
        wantedPosition.y = (wantedPosition.y < minY) ? minY : wantedPosition.y;
        wantedPosition.y = (wantedPosition.y > maxY) ? maxY : wantedPosition.y;

        // set the camera to go to the wanted position in a certain amount of time
        transform.position = Vector3.Lerp(transform.position, wantedPosition, (Time.deltaTime * damping));
    }

    public void FixCamera()
    {

    }

    public void SmoothTransition()
    {

    }

    public void PanOut()
    {

    }

    public void PanIn()
    {

    }
}
