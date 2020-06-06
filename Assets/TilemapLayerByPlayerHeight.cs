using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapLayerByPlayerHeight : MonoBehaviour
{
    private Jump _jump;
    void Start()
    {
        _jump = GameObject.FindGameObjectWithTag("Player").GetComponent<Jump>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
