using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach this to a shadow gameObject

public class Shadow : MonoBehaviour
{
    public Vector3 anchorAdjustment;

    //shadow relies on Jump.cs, which is in the parent
    private Jump _jump;
    private Vector3 localOffset;
    private void Start()
    {
        anchorAdjustment = gameObject.transform.localPosition;
        _jump = GetComponentInParent<Jump>();
        localOffset = new Vector3(0, -0.1f, 0);
    }

    private void Update()
    {
        gameObject.transform.position = _jump.projectedLanding - _jump.offset + localOffset;

        ShadowSizeAdjustment();
    }

    /// <summary>
    /// Change size depending on how far away from the actor the shadow is.
    /// </summary>
    private void ShadowSizeAdjustment()
    { 

        Vector3 dis = gameObject.transform.parent.transform.position - gameObject.transform.position;
        float xScale = 1 / (Mathf.Abs(dis.y) + 1); //y = 1/(x+1)
        gameObject.transform.localScale = new Vector3(
            xScale,
            gameObject.transform.localScale.y,
            gameObject.transform.localScale.z);
    }

}
