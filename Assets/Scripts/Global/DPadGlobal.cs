using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPadGlobal : MonoBehaviour
{
    public bool DPadUp;
    public bool DPadDown;
    public bool DPadRight;
    public bool DPadLeft;
    public bool AButton;
    public bool BButton;
    void Start()
    {
        DPadUp = false;
        DPadDown = false;
        DPadLeft = false;
        DPadRight = false;
        AButton = false;
        BButton = false;
    }
}
