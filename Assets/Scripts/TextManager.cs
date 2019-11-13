using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D coll) {
        
        //Debug.Log("Something touched!");

        if (coll.tag == "Player")
        {
            Debug.Log("We are in a dialogue box.");
        
        }

    }
}
