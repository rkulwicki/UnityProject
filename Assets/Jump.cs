using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TilemapFunctions;

public class Jump : MonoBehaviour
{

    public int floorBelow;

    private BattleManager _battlemanager;
    // Start is called before the first frame update
    void Start()
    {
        _battlemanager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        floorBelow = GetOrderInLayerOfFloorBelow(transform.position);
        if (_battlemanager.state == BattleState.INACTIVE) {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                
            }

        }
    }
}
