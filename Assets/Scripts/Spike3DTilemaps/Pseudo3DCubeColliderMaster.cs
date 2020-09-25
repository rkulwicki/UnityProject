using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;

public class Pseudo3DCubeColliderMaster : MonoBehaviour
{
    public bool canMoveXPositive, canMoveXNegative, canMoveYPositive, canMoveYNegative, canMoveZPositive, canMoveZNegative;
    public GameObject xpLock, xnLock, ypLock, ynLock, zpLock, znLock;
    public Transform prefab;

    public float buffer;

    public List<Vector3> tileWorldLocations;

    private GameObject[] tilemapGameObjects;
    private GameObject player;

    void Start()
    {
        var list = new List<GameObject>();
        foreach (Transform child in GameObject.FindGameObjectWithTag("Grid").transform)
        {
            if(child.gameObject.tag == "Tilemap" && (child.gameObject.GetComponent<TilemapRenderer>().sortingLayerName == "FloorBelow" ||
                                                     child.gameObject.GetComponent<TilemapRenderer>().sortingLayerName == "FloorAbove"))
                list.Add(child.gameObject);
        }
        tilemapGameObjects = list.ToArray();

        //setup each tilemap with colliders
        foreach(var tmgo in tilemapGameObjects)
        {
            SetUp3DTiles(tmgo.GetComponent<Tilemap>(), tmgo.GetComponent<TilemapRenderer>().sortingOrder);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        UpdatePlayerCanMove();
    }

    /// <summary>
    /// Sets up the tiles on a tilemap with the Pseudo 3D Cube Collider
    /// </summary>
    /// <param name="tilemap"></param>
    /// <param name="sortingOrder"></param>
    public void SetUp3DTiles(Tilemap tilemap, int sortingOrder)
    {
        tileWorldLocations = new List<Vector3>();

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = tilemap.CellToWorld(localPlace);
            if (tilemap.HasTile(localPlace))
            {
                tileWorldLocations.Add(place);
                //Debug.Log(place);

                var psuedo3DTileCollider = Instantiate(prefab, this.transform, true);
                psuedo3DTileCollider.transform.position = new Vector3(place.x, place.y, 0f);
                psuedo3DTileCollider.GetComponent<Pseudo3DCubeCollider>().sortingOrder = sortingOrder;
                psuedo3DTileCollider.GetComponent<Pseudo3DCubeCollider>().tilePosition = new Vector2Int(Convert.ToInt32(place.x), Convert.ToInt32(place.y) - (sortingOrder - 1));
                psuedo3DTileCollider.GetComponent<Pseudo3DCubeCollider>().CreatePseudo3DCubeCollider();
                psuedo3DTileCollider.GetComponent<Pseudo3DCubeCollider>().buffer = buffer;
            }
        }
    }

    private void UpdatePlayerCanMove()
    {
        player.GetComponent<TestPlayer>().canMoveXNegative = canMoveXNegative;
        player.GetComponent<TestPlayer>().canMoveXPositive = canMoveXPositive;
        player.GetComponent<TestPlayer>().canMoveYNegative = canMoveYNegative;
        player.GetComponent<TestPlayer>().canMoveYPositive = canMoveYPositive;
        player.GetComponent<TestPlayer>().canMoveZNegative = canMoveZNegative;
        player.GetComponent<TestPlayer>().canMoveZPositive = canMoveZPositive;
    }
}
