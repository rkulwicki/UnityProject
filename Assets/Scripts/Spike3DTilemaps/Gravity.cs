using UnityEngine;
using System.Collections;
using static Globals;

//before dragging and dropping:
// needs a reference to a pseudo3DPosition
// and a refrerence to a "canMove"

    //NOTE - THIS IS ONLY SET UP FOR A PLAYER GAMEOBJECT
public class Gravity : MonoBehaviour
{
    public bool canMoveZNegative;
    public Vector3 pseudo3DPosition;
    public float speed;
    public float timer;
    public float timeToStopAcceleration;
    public bool isGravityActive;

    // Update is called once per frame
    void Update()
    {
        canMoveZNegative = this.gameObject.GetComponent<Pseudo3DPlayer>().canMoveZNegative;
        if (canMoveZNegative)
        {
            isGravityActive = true;
            if (timer < 2)
                timer += Time.deltaTime;
            
            this.gameObject.GetComponent<Pseudo3DPlayer>().MoveZNegative(speed*timer);
        }
        else
        {
            isGravityActive = false;
            timer = 0;
        }
    }
}