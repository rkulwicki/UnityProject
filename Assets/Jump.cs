using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TilemapFunctions;

public class Jump : MonoBehaviour
{

    public int floorBelow;

    public bool jumping;

    private BattleManager _battlemanager;

    public float timeToJump, heightOfJump; 
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

            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
            {
                GameObject objectToMove     = this.gameObject;
                float height                = heightOfJump;
                float seconds               = timeToJump;
                int startingFloorOrder      = GetOrderInLayerOfFloorBelow(this.gameObject.transform.position);

                StartCoroutine(JumpAction(objectToMove, height, seconds, startingFloorOrder));
            }

        }
    }


    //move up

    private IEnumerator JumpAction(GameObject objectToMove, float height, float seconds, int startingFloorOrder) //move over time
    {
        jumping = true;

        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        Vector3 copyOfStartingPos = startingPos;
        while (elapsedTime < seconds) //up
        {
            //third parameter is like the x axis of a graph (usually over time) from 0 to 1
            //Vector3.Lerp(startingPos, end, (elapsedTime / seconds));

            var amountToMove = (Time.deltaTime * height)/seconds; //change in time
            objectToMove.transform.position += new Vector3(0,amountToMove,0);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        elapsedTime = 0;
        Vector3 newStartingPos = objectToMove.transform.position;
        while (elapsedTime < (seconds)) //down
        {
            var amountToMove = (Time.deltaTime * height) / seconds; //change in time
            objectToMove.transform.position -= new Vector3(0, amountToMove, 0);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        jumping = false;
    }

}
