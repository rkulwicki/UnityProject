using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TilemapFunctions;

public class Jump : MonoBehaviour
{
    public int floorGrounded;

    public int floorBelow;

    public bool jumping;

    public Vector3 projectedLanding;

    private BattleManager _battlemanager;

    public float timeToJump, heightOfJump;

    public float playerHeight;
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
        if (!jumping)
        {
            floorGrounded = GetOrderInLayerOfFloorBelow(transform.position);
            playerHeight = floorGrounded;
        }
    }


    //move up

    private IEnumerator JumpAction(GameObject objectToMove, float height, float seconds, int startingFloorOrder) //move over time
    {
        jumping = true;

        projectedLanding = objectToMove.transform.position;

        float elapsedTime = 0;

        var sum = 0f;

        //up
        while (elapsedTime < seconds)
        {
            //float x = elapsedTime;
            //float amountToMove = (height * (x) * (x - seconds) + 0.25f) / 10f; //change in time
            
            
            var amountToMove = (Time.deltaTime * height)/seconds; //change in time
            objectToMove.transform.position += new Vector3(0, amountToMove, 0);
            elapsedTime += Time.deltaTime;

            sum += amountToMove;
            projectedLanding = objectToMove.transform.position - new Vector3(0, sum, 0);

            playerHeight = startingFloorOrder + sum;

            yield return new WaitForEndOfFrame();
        }

        //down
        elapsedTime = 0;
        Vector3 newStartingPos = objectToMove.transform.position;
        while (playerHeight > floorBelow) // (elapsedTime < (seconds)) 
        {

            //if (playerHeight <= floorBelow)
            //    break; //you landed!

            var amountToMove = (Time.deltaTime * height) / seconds; //change in time
            objectToMove.transform.position -= new Vector3(0, amountToMove, 0);
            elapsedTime += Time.deltaTime;

            sum -= amountToMove;
            projectedLanding = objectToMove.transform.position - new Vector3(0, sum, 0);

            playerHeight = startingFloorOrder + sum;

            yield return new WaitForEndOfFrame();
        }

        //what floor did we land on?
        floorGrounded = GetOrderInLayerOfFloorBelow(transform.position);
        jumping = false;
    }

}
