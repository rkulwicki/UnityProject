using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class WallTilemapsFunctionality : MonoBehaviour
{

    public GameObject[] wallTilemapGameObjects;  //arrays are faster

    public double y;

    // These locks are so we don't set to "can move" to true by any other tilemap.
    TODO
    public bool canMoveLocked;
    public GameObject tilemapWithLock;
    //------------

    private MovementInfo _moveInfo;
    private PlayerMove _playerMove;
    private float projectedLanding;

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
        AddWallTilemapComponent();

        _moveInfo = GameObject.FindGameObjectWithTag("Player").GetComponent<MovementInfo>();
        _playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();

    }

    // Update is called once per frame
    void Update()
    {
        ChangeWallTilemapLayerByPlayerY();
        ChangeWallTilemapsFunctionalityByLayer();
    }

    private void AddWallTilemapComponent()
    {
        foreach (var tm in wallTilemapGameObjects)
        {
            tm.gameObject.AddComponent<WallTilemapScript>();
        }
    }

    private void ChangeWallTilemapLayerByPlayerY()
    {
        //TODO
        y = _moveInfo.GlobalPosition.y + 0.5;

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
                //tm.GetComponent<TilemapCollider2D>().enabled = true;
            }
            if (tm.GetComponent<TilemapRenderer>().sortingLayerName == "WallFront")
            {
                //tm.GetComponent<TilemapCollider2D>().enabled = false;

            }
        }
    }
}
