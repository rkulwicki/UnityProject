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
                Vector3 end                 = (this.gameObject.transform.position + new Vector3(0, heightOfJump, 0));
                float seconds               = timeToJump;
                int startingFloorOrder      = GetOrderInLayerOfFloorBelow(this.gameObject.transform.position);

                StartCoroutine(JumpAction(objectToMove, end, seconds, startingFloorOrder));
            }

        }
    }


    //move up

    private IEnumerator JumpAction(GameObject objectToMove, Vector3 end, float seconds, int startingFloorOrder) //move over time
    {

        var distanceToTravel = end;

        jumping = true;
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        Vector3 copyOfStartingPos = startingPos;
        while (elapsedTime < (seconds/2)) //up
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        elapsedTime = 0;
        Vector3 newStartingPos = objectToMove.transform.position;
        while (elapsedTime < (seconds)) //down
        {
            objectToMove.transform.position = Vector3.Lerp(newStartingPos, copyOfStartingPos, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        jumping = false;
    }

}
