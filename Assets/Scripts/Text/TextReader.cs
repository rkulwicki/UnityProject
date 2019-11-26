using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextReader : MonoBehaviour
{
    public TextAsset textFile;
    public string[] lines;
    private void Start()
    {
        if (textFile == null) //error handling
            return;

        lines = ParseTextFileByNewline(textFile);
    }
    //return text from textFile
    public string[] ParseTextFileByNewline(TextAsset textFile)
    {
        string[] lines = textFile.text.Split(new string[] { "\n" }, StringSplitOptions.None);
        return lines;
    }
}
