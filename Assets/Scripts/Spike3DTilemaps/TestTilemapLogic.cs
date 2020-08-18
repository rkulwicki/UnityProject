using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class TestTilemapLogic : MonoBehaviour
{
    private GameObject[] tilemapGameObjects; 
    public Vector3 _pseudo3DPosition;

    void Start()
    {
        var list = new List<GameObject>();
        foreach (Transform child in transform)
        {
            list.Add(child.gameObject);
        }
        tilemapGameObjects = list.ToArray();
    }

    void Update()
    {
        _pseudo3DPosition = GameObject.Find("TestPlayer").GetComponent<TestPlayer>().pseudo3DPosition;
        ChangeTilemapLayerByPlayerHeight();
    }

    private void ChangeTilemapLayerByPlayerHeight()
    {
        foreach (var tm in tilemapGameObjects)
        {
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "FloorBelow" && _pseudo3DPosition.z < tm.GetComponent<TilemapRenderer>().sortingOrder)
                tm.GetComponent<TilemapRenderer>().sortingLayerName = "FloorAbove";

            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "FloorAbove" && _pseudo3DPosition.z >= tm.GetComponent<TilemapRenderer>().sortingOrder)
                tm.GetComponent<TilemapRenderer>().sortingLayerName = "FloorBelow";
        }
    }
}
