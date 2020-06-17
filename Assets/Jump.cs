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
    public CapsuleCollider2D _cap2D;
    public int difInFloors;
    private Vector3 offset;
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
        if (_battlemanager.state == BattleState.INACTIVE) {

            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
            {
                GameObject objectToMove     = this.gameObject;
                float height                = heightOfJump;
                float seconds               = timeToJump;
                int startingFloorOrder      = GetOrderOfTilemapAtPosition(this.gameObject.transform.position + offset);

                StartCoroutine(JumpAction(objectToMove, height, seconds, startingFloorOrder));
            }

        }
        if (!jumping)
        {
            floorGrounded = GetOrderOfTilemapAtPosition(transform.position + offset);
            floorBelow = floorGrounded;
            playerHeight = floorGrounded;
        }
    }


    //move up

    private IEnumerator JumpAction(GameObject objectToMove, float height, float seconds, int startingFloorOrder) //move over time
    {
        jumping = true;

        projectedLanding = objectToMove.transform.position; // + offset;

        float elapsedTime = 0;

        var sum = 0f;
        difInFloors = 0;

        //up
        while (elapsedTime < seconds)
        {
            //getting the projected Landing is hard man
            var b = objectToMove.transform.position; // + offset; //actor's bottom
            difInFloors = floorBelow - floorGrounded; 
            projectedLanding = b - new Vector3(0, sum, 0) + new Vector3(0, difInFloors, 0);

            var amountToMove = (Time.deltaTime * height)/seconds; //change in time
            objectToMove.transform.position += new Vector3(0, amountToMove, 0);
            elapsedTime += Time.deltaTime;

            sum += amountToMove;
            
            floorBelow = GetOrderOfTilemapAtPosition(projectedLanding); //TODO PROJECTED LANDING MESSED UP???

            playerHeight = startingFloorOrder + sum;

            yield return new WaitForEndOfFrame();
        }

        //down
        elapsedTime = 0;

        while (playerHeight > floorBelow || IsOnWallTilemap(transform.position + offset)) // (elapsedTime < (seconds)) 
        {
            //getting the projected Landing is hard man
            var b = objectToMove.transform.position; //+ offset; //actor's bottom
            difInFloors = floorBelow - floorGrounded;
            projectedLanding = objectToMove.transform.position - new Vector3(0, sum, 0) + new Vector3(0, difInFloors, 0);
            floorBelow = GetOrderOfTilemapAtPosition(projectedLanding);

            var amountToMove = (Time.deltaTime * height) / seconds; //change in time
            objectToMove.transform.position -= new Vector3(0, amountToMove, 0);
            elapsedTime += Time.deltaTime;

            sum -= amountToMove;

            playerHeight = startingFloorOrder + sum;

            yield return new WaitForEndOfFrame();
        }

        //what floor did we land on?
        floorGrounded = GetOrderOfTilemapAtPosition(transform.position);
        jumping = false;
    }

    //need to find out when changing between tilemaps?
    //1. What one did we start on?
}
