using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPadGlobal : MonoBehaviour
{
    public bool DPadUp;
    public bool DPadDown;
    public bool DPadRight;
    public bool DPadLeft;
    void Start()
    {
        DPadUp = false;
        DPadDown = false;
        DPadLeft = false;
        DPadRight = false;
    }
}
