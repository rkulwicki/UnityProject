using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this to a shadow gameObject

public class Shadow : MonoBehaviour
{
    public Vector3 anchorAdjustment;
    
    //shadow relies on Jump.cs, which is in the parent
    private Jump _jump;

    private void Start()
    {
        anchorAdjustment = gameObject.transform.localPosition;
        _jump = GetComponentInParent<Jump>();
    }

    private void Update()
    {
        if (!_jump.jumping)
        {
            gameObject.transform.position = gameObject.transform.parent.transform.position + anchorAdjustment;
        }
        else
        {
            gameObject.transform.position = _jump.projectedLanding + anchorAdjustment;
        }
    }

}
