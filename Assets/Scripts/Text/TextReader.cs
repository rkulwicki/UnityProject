using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReader : MonoBehaviour
{
    public TextAsset textFile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //return text from textFile
    public string[] ParseTextFileByNewline(TextAsset textFile)
    {
        var lines = textFile.text.Split(new string[] { "\\n" }, StringSplitOptions.None);
        return lines;
    }
}
