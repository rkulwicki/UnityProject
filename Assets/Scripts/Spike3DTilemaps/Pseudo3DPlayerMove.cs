using UnityEngine;
using System.Collections;
using static Pseudo3DTilemapLogic;
using static Globals;

public class Pseudo3DPlayerMove : MonoBehaviour
{
    public float speed;
    public bool canMoveXPositive, canMoveXNegative, canMoveYPositive, canMoveYNegative, canMoveZPositive, canMoveZNegative;

    public Vector3 pseudo3DPosition;

    public bool isPlayerMovementSuspended;

    protected void JustMove()
    {
        if (!isPlayerMovementSuspended)
        {
            MoveGlobalPosition();
            MoveTransformXandY();
            AdjustTransformYBasedOnPseudo3DZ();
            UpdatePseudo3DPosition();
        }
    }

    protected void MoveGlobalPosition()
    {
        //x
        if (Input.GetKey(XPositiveKey) && canMoveXPositive)
            MoveXPositive(speed);
        else if (Input.GetKey(XNegativeKey) && canMoveXNegative)
            MoveXNegative(speed);

        //y
        if (Input.GetKey(YPositiveKey) && canMoveYPositive)
            MoveYPositive(speed);
        else if (Input.GetKey(YNegativeKey) && canMoveYNegative)
            MoveYNegative(speed);

        //z
        if (Input.GetKey(ZPositiveKey) && canMoveZPositive)
            MoveZPositive(speed);
        else if (Input.GetKey(ZNegativeKey) && canMoveZNegative)
            MoveZNegative(speed);
    }

    protected void MoveTransformXandY()
    {
        transform.position = new Vector3(pseudo3DPosition.x, pseudo3DPosition.y, 0);
    }

    protected void AdjustTransformYBasedOnPseudo3DZ()
    {
        transform.position += new Vector3(0, pseudo3DPosition.z, 0);
    }

    protected void UpdatePseudo3DPosition()
    {
        GetComponent<Pseudo3DPosition>().pseudo3DPosition = pseudo3DPosition;
        GetComponent<Pseudo3DPosition>().distanceFromGround = GetDistanceFromGround(pseudo3DPosition);
    }

    #region Public Methods

    public void MoveXPositive(float speed)
    {
        pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(1, 0, 0), Time.deltaTime * speed);
    }
    public void MoveXNegative(float speed)
    {
        pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(-1, 0, 0), Time.deltaTime * speed);
    }
    public void MoveYPositive(float speed)
    {
        pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 1, 0), Time.deltaTime * speed);
    }

    public void MoveYNegative(float speed)
    {
        pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, -1, 0), Time.deltaTime * speed);
    }
    public void MoveZPositive(float speed)
    {
        pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 0, 1), Time.deltaTime * speed);
    }
    public void MoveZNegative(float speed)
    {
        pseudo3DPosition = Vector3.MoveTowards(pseudo3DPosition, pseudo3DPosition + new Vector3(0, 0, -1), Time.deltaTime * speed);
    }
    #endregion
}
