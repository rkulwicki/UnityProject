using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderHitScript : MonoBehaviour
{
    public bool InCollider;

    private bool _stay;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Floor")
        {
            InCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            InCollider = false;
        }
    }

}
