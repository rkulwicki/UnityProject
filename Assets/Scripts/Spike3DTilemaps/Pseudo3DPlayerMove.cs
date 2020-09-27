using UnityEngine;
using System.Collections;

public class Pseudo3DPlayerMove : MonoBehaviour
{
    public float speed;
    public string xPositiveKey, xNegativeKey, yPositiveKey, yNegativeKey, zPositiveKey, zNegativeKey;
    public bool canMoveXPositive, canMoveXNegative, canMoveYPositive, canMoveYNegative, canMoveZPositive, canMoveZNegative;

    private Vector3 _pseudo3DPosition;
    private int _distanceFromGround;

    protected void JustMove()
    {
        MoveGlobalPosition();
        MoveTransformXandY();
        AdjustTransformYBasedOnPseudo3DZ();
        UpdatePseudo3DPosition();
    }

    protected void MoveGlobalPosition()
    {
        //x
        if (Input.GetKey(xPositiveKey) && canMoveXPositive)
            _pseudo3DPosition = Vector3.MoveTowards(_pseudo3DPosition, _pseudo3DPosition + new Vector3(1, 0, 0), Time.deltaTime * speed);
        else if (Input.GetKey(xNegativeKey) && canMoveXNegative)
            _pseudo3DPosition = Vector3.MoveTowards(_pseudo3DPosition, _pseudo3DPosition + new Vector3(-1, 0, 0), Time.deltaTime * speed);

        //y
        if (Input.GetKey(yPositiveKey) && canMoveYPositive)
            _pseudo3DPosition = Vector3.MoveTowards(_pseudo3DPosition, _pseudo3DPosition + new Vector3(0, 1, 0), Time.deltaTime * speed);
        else if (Input.GetKey(yNegativeKey) && canMoveYNegative)
            _pseudo3DPosition = Vector3.MoveTowards(_pseudo3DPosition, _pseudo3DPosition + new Vector3(0, -1, 0), Time.deltaTime * speed);

        //z
        if (Input.GetKey(zPositiveKey) && canMoveZPositive)
            _pseudo3DPosition = Vector3.MoveTowards(_pseudo3DPosition, _pseudo3DPosition + new Vector3(0, 0, 1), Time.deltaTime * speed);
        else if (Input.GetKey(zNegativeKey) && canMoveZNegative)
            _pseudo3DPosition = Vector3.MoveTowards(_pseudo3DPosition, _pseudo3DPosition + new Vector3(0, 0, -1), Time.deltaTime * speed);
    }

    protected void MoveTransformXandY()
    {
        transform.position = new Vector3(_pseudo3DPosition.x, _pseudo3DPosition.y, 0);
    }

    protected void AdjustTransformYBasedOnPseudo3DZ()
    {
        transform.position += new Vector3(0, _pseudo3DPosition.z, 0);
    }

    protected void UpdatePseudo3DPosition()
    {
        GetComponent<Pseudo3DPosition>().pseudo3DPosition = _pseudo3DPosition;
        GetComponent<Pseudo3DPosition>().distanceFromGround = _distanceFromGround;
    }
}
