using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static TilemapFunctions;


//TODO 
//1. When jump buffer is set to 0.1,  we can "fall" off of edges, but when you move to a lower tilemap, you don't "fall". Fix.
//2. When jump buffer is set to 0.1,  you can clip into a higher tilemap. Fix. Add raycast to the corners to prevent clipping.
//3. When jumping in the down direction from a lower to a higher tilemap, the jitters and the "projected landing" increases in the y direction.

public class Jump : MonoBehaviour
{
    public int floorGrounded;

    public int floorBelow, previousFloor;

    public bool jumping, falling;

    public Vector3 projectedLanding;

    private BattleManager _battlemanager;

    public float timeToJump, heightOfJump;

    public float playerZHeight;
    public CapsuleCollider2D _cap2D;
    public int difInFloors;
    public Vector3 offset;

    private Vector3 transformPlusOffset;
    // Start is called before the first frame update
    void Start()
    {
        _cap2D = gameObject.GetComponent<CapsuleCollider2D>();
        _battlemanager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        offset = new Vector3(0, -0.5f, 0);
    }

    // Update is called once per frame
    void Update()
    {

        transformPlusOffset = transform.position + offset;//GetProjectedLanding(transform.position + offset, playerZHeight);
        projectedLanding = GetProjectedLanding(transform.position + offset, playerZHeight); //WIP

        if (_battlemanager.state == BattleState.INACTIVE) {

            if (Input.GetKeyDown(KeyCode.Space) && !jumping && !falling)
            {
                GameObject objectToMove     = this.gameObject;
                float height                = heightOfJump;
                float seconds               = timeToJump;
                int startingFloorOrder      = GetOrderOfTilemapAtPosition(this.gameObject.transform.position + offset);

                StartCoroutine(JumpAction(objectToMove, height, seconds, startingFloorOrder));
            }

        }
        if (!jumping && !falling)
        {
            //check for falling if floor below was different than it was before.
            previousFloor = floorBelow;

            floorGrounded = GetOrderOfTilemapAtPosition(transform.position + offset);
            floorBelow = floorGrounded;

            if (floorBelow != previousFloor)
                StartCoroutine(Fall(previousFloor, floorBelow, this.gameObject, heightOfJump, timeToJump));

            if (!falling)
                playerZHeight = floorGrounded; //needs to go after "Fall"
        }
    }

    
    //SOMETHING WIERD IS GOING ON WHEN THE PLAYER'S POSITION IS OVER HALF THE TOP EDGE


    private IEnumerator JumpAction(GameObject objectToMove, float height, float seconds, int startingFloorOrder) //move over time
    {
        jumping = true;

        float elapsedTime = 0;

        var sum = 0f;
        difInFloors = 0;

        //up
        while (elapsedTime < seconds)
        {
            difInFloors = floorBelow - floorGrounded; 

            var amountToMove = (Time.deltaTime * height)/seconds; //change in time
            objectToMove.transform.position += new Vector3(0, amountToMove, 0);
            elapsedTime += Time.deltaTime;

            sum += amountToMove;
            
            floorBelow = GetOrderOfTilemapAtPosition(transformPlusOffset);

            playerZHeight = startingFloorOrder + sum;

            yield return new WaitForEndOfFrame();
        }

        //down
        elapsedTime = 0;

        while (playerZHeight > floorBelow || IsOnWallTilemap(objectToMove.transform.position + offset)) // (elapsedTime < (seconds)) 
        {

            difInFloors = floorBelow - floorGrounded;

            floorBelow = GetOrderOfTilemapAtPosition(transformPlusOffset);

            var amountToMove = (Time.deltaTime * height) / seconds; //change in time
            objectToMove.transform.position -= new Vector3(0, amountToMove, 0);
            elapsedTime += Time.deltaTime;

            sum -= amountToMove;

            playerZHeight = startingFloorOrder + sum;

            yield return new WaitForEndOfFrame();
        }

        //what floor did we land on?
        floorGrounded = GetOrderOfTilemapAtPosition(transform.position);
        jumping = false;
    }
    
    public IEnumerator Fall(int previousFloorBelow, int floorBelow, GameObject objectToMove, float height, float seconds)
    {
        falling = true;

        var sum = 0f;
        float elapsedTime = 0;

        //change transform until the current floor below is less
        while (playerZHeight > floorBelow || IsOnWallTilemap(objectToMove.transform.position + offset))
        {
            difInFloors = floorBelow - floorGrounded;

            floorBelow = GetOrderOfTilemapAtPosition(transformPlusOffset);

            var amountToMove = (Time.deltaTime * height) / seconds; //change in time
            objectToMove.transform.position -= new Vector3(0, amountToMove, 0);
            elapsedTime += Time.deltaTime;

            sum -= amountToMove;

            playerZHeight = previousFloorBelow + sum;

            yield return new WaitForEndOfFrame();
        }

        //what floor did we land on?
        floorGrounded = GetOrderOfTilemapAtPosition(transform.position);
        falling = false;
    }

}
