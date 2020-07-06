using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHelperScript : MonoBehaviour
{
    public bool collideFromLeft;
    public bool collideFromTop;
    public bool collideFromRight;
    public bool collideFromBottom;

    private ColliderHitScript _topCol, _rightCol, _bottomCol, _leftCol;

    // Start is called before the first frame update
    void Start()
    {
        _topCol = gameObject.transform.Find("TopColliderObject").gameObject.GetComponent<ColliderHitScript>();
        _rightCol = gameObject.transform.Find("RightColliderObject").gameObject.GetComponent<ColliderHitScript>();
        _bottomCol = gameObject.transform.Find("BottomColliderObject").gameObject.GetComponent<ColliderHitScript>();
        _leftCol = gameObject.transform.Find("LeftColliderObject").gameObject.GetComponent<ColliderHitScript>();
    }

    private void Update()
    {
        collideFromTop = _topCol.InCollider;
        collideFromRight = _rightCol.InCollider;
        collideFromBottom = _bottomCol.InCollider;
        collideFromLeft = _leftCol.InCollider;
    }

}
