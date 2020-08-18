using UnityEngine;
using System.Collections;

public class TestPlayerMove : MonoBehaviour
{
    public float speed;
    public Vector3 pseudo3DPosition;
    public string xPositiveKey, xNegativeKey, yPositiveKey, yNegativeKey, zPositiveKey, zNegativeKey;
    public bool canMoveXPositive, canMoveXNegative, canMoveYPositive, canMoveYNegative, canMoveZPositive, canMoveZNegative;

    protected void JustMove()
    {
        MoveGlobalPosition();
        MoveTransformXandY();
        AdjustTransformYBasedOnPseudo3DZ();
    }

    protected void MoveGlobalPosition()
    {
        //x
        if (Input.GetKey(xPositiveKey) && canMoveXPositive)
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(1, 0, 0), Time.deltaTime * speed);
        else if (Input.GetKey(xNegativeKey) && canMoveXPositive)
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(-1, 0, 0), Time.deltaTime * speed);

        //y
        if (Input.GetKey(yPositiveKey) && canMoveYPositive)
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 1, 0), Time.deltaTime * speed);
        else if (Input.GetKey(yNegativeKey) && canMoveYNegative)
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, -1, 0), Time.deltaTime * speed);

        //z
        if (Input.GetKey(zPositiveKey) && canMoveZPositive)
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 0, 1), Time.deltaTime * speed);
        else if (Input.GetKey(zNegativeKey) && canMoveZNegative)
            pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 0, -1), Time.deltaTime * speed);
    }

    protected void MoveTransformXandY()
    {
        transform.position = new Vector3(pseudo3DPosition.x, pseudo3DPosition.y, 0);
    }

    protected void AdjustTransformYBasedOnPseudo3DZ()
    {
        transform.position += new Vector3(0, pseudo3DPosition.z, 0);
    }
}
