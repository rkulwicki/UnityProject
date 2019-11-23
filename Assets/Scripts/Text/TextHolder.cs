using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHolder : MonoBehaviour
{
    public string text;
    // Start is called before the first frame update
    void Start()
    {
        Transform trans = gameObject.transform;
        var childTrans = trans.Find("TextBox"); //access text
        var grandchildTrans = childTrans.Find("Text");
        grandchildTrans.GetComponent<UnityEngine.UI.Text>().text = text;
        
    }
}
