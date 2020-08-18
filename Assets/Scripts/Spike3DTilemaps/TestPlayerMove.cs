using UnityEngine;
using System.Collections;

public class TestPlayerMove : MonoBehaviour
{
    public float speed;
    public Vector3 pseudo3DPosition;
    public string xPositiveKey, xNegativeKey, yPositiveKey, yNegativeKey, zPositiveKey, zNegativeKey;

    protected void JustMove()
    {
        MoveGlobalPosition();
        transform.position = new Vector3(pseudo3DPosition.x, pseudo3DPosition.y, 0);
    }

    protected void MoveGlobalPosition()
    {
        //x
        if (Input.GetKey(xPositiveKey))
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(1, 0, 0), Time.deltaTime * speed);
        else if (Input.GetKey(xNegativeKey))
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(-1, 0, 0), Time.deltaTime * speed);

        //y
        if (Input.GetKey(yPositiveKey))
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 1, 0), Time.deltaTime * speed);
        else if (Input.GetKey(yNegativeKey))
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, -1, 0), Time.deltaTime * speed);

        //z
        if (Input.GetKey(zPositiveKey))
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 0, 1), Time.deltaTime * speed);
        else if (Input.GetKey(zNegativeKey))
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 0, -1), Time.deltaTime * speed);
    }
}
