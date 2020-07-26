using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class WallTilemapsFunctionality : MonoBehaviour
{

    public GameObject[] wallTilemapGameObjects;  //arrays are faster

    public double y;

    private double distanceAboveGround;
    private GameObject player;

    void Start()
    {
        var list = new List<GameObject>();
        foreach (Transform child in transform)
        {
            if(child.gameObject.GetComponent<TilemapRenderer>().sortingLayerName == "Wall" ||
               child.gameObject.GetComponent<TilemapRenderer>().sortingLayerName == "WallFront")
            list.Add(child.gameObject);
        }
        wallTilemapGameObjects = list.ToArray();

        player = GameObject.FindGameObjectWithTag("Player");
        distanceAboveGround = player.GetComponent<Jump>().distanceAboveGround;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWallTilemapLayerByPlayerY();
        ChangeWallTilemapsFunctionalityByLayer();
    }

    private void ChangeWallTilemapLayerByPlayerY()
    {
        y = player.transform.position.y - distanceAboveGround;
        foreach (var tm in wallTilemapGameObjects)
        {
            if (y > tm.GetComponent<TilemapRenderer>().sortingOrder) //behind wall
                tm.GetComponent<TilemapRenderer>().sortingLayerName = "WallFront";
            else
                tm.GetComponent<TilemapRenderer>().sortingLayerName = "Wall";
        }
    }

    private void ChangeWallTilemapsFunctionalityByLayer()
    {
        foreach (var tm in wallTilemapGameObjects)
        {
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "Wall")
            {
                //continue as normal
                tm.GetComponent<TilemapCollider2D>().enabled = true;
            }
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "WallFront")
            {
                tm.GetComponent<TilemapCollider2D>().enabled = false;

            }
        }
    }
}
