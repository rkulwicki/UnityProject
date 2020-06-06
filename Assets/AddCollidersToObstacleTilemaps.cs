using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AddCollidersToObstacleTilemaps : MonoBehaviour
{

    public GameObject[] tilemapGameObjects;  //arrays are faster

    void Start()
    {
        var list = new List<GameObject>();
        foreach(Transform child in transform)
        {
            list.Add(child.gameObject);
        }
        tilemapGameObjects = list.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var tm in tilemapGameObjects)
        {
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "Obstacles" &&
                !tm.GetComponent<TilemapCollider2D>().enabled)
            {
                tm.GetComponent<TilemapCollider2D>().enabled = true;
            }
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "Floor" &&
                tm.GetComponent<TilemapCollider2D>().enabled)
            {
                tm.GetComponent<TilemapCollider2D>().enabled = false;
            }
        }
    }
}
