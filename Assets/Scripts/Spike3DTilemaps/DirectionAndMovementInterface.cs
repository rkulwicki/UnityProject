using UnityEngine;
using System.Collections;

public class DirectionAndMovementInterface : MonoBehaviour
{
    public Vector3 pseudo3DVelocity;
    public XYDirection direction;

    private Vector3 _lastPosition;
    private Pseudo3DPlayer _pseudo3DPlayer;
    private XYDirection _lastDirection;
    // Use this for initialization
    void Start()
    {
        _pseudo3DPlayer = gameObject.GetComponent<Pseudo3DPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        pseudo3DVelocity = (_pseudo3DPlayer.pseudo3DPosition - _lastPosition) / Time.deltaTime;
        _lastPosition = _pseudo3DPlayer.pseudo3DPosition;
        _lastDirection = direction;

        if (pseudo3DVelocity.y > 0) //up
        {
            if(pseudo3DVelocity.x > 0)
            {
                direction = XYDirection.XPosYPos;
            }
            else if (pseudo3DVelocity.x < 0)
            {
                direction = XYDirection.XNegYPos;
            }
            else //x == 0
            {
                if(_lastDirection == XYDirection.XPosYNeg || _lastDirection == XYDirection.XPosYPos)
                {
                    direction = XYDirection.XPosYPos;
                }
                else
                {
                    direction = XYDirection.XNegYPos;
                }
            }
        }
        else if(pseudo3DVelocity.y < 0) //down
        {
            if (pseudo3DVelocity.x > 0)
            {
                direction = XYDirection.XPosYNeg;
            }
            else if (pseudo3DVelocity.x < 0)
            {
                direction = XYDirection.XNegYNeg;
            }
            else //x == 0
            {
                if (_lastDirection == XYDirection.XPosYNeg || _lastDirection == XYDirection.XPosYPos)
                {
                    direction = XYDirection.XPosYNeg;
                }
                else
                {
                    direction = XYDirection.XNegYNeg;
                }
            }
        }
        else //y == 0
        {
            if (pseudo3DVelocity.x > 0)
            {
                //should almost always be facing front if just moving right
                direction = XYDirection.XPosYNeg;
            }
            else if (pseudo3DVelocity.x < 0)
            {
                direction = XYDirection.XNegYNeg;
            }
            else //x == 0
            {
                _lastDirection = direction;
            }
        }

        //if (pseudo3DVelocity.x > 0 && pseudo3DVelocity.y <= 0.25)
        //    direction = XYDirection.XPosYNeg;
        //else if(pseudo3DVelocity.x < 0 && pseudo3DVelocity.y <= 0.25)
        //    direction = XYDirection.XNegYNeg;
        //else if (pseudo3DVelocity.x > 0 && pseudo3DVelocity.y >= 0.25)
        //    direction = XYDirection.XPosYPos;
        //else if (pseudo3DVelocity.x < 0 && pseudo3DVelocity.y >= 0.25)
        //    direction = XYDirection.XNegYPos;
    }
}

public enum XYDirection { XPosYPos, XPosYNeg, XNegYPos, XNegYNeg}