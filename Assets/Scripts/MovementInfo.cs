﻿using UnityEngine;
using System.Collections;
using static TilemapFunctions;

public class MovementInfo : MonoBehaviour
{
    public Vector3 GlobalPosition; //{ private set; get; }
    public float Z { set; private get; }

    public float playerHeight;

    public Vector3 velocity;
    public bool upFacing;
    public bool rightFacing;
    public bool isMoving;

    public float movingBuffer = 0.1f;

    private Vector3 _lastPosition;
    private Jump _jump;

    private void Start()
    {
        _jump = gameObject.GetComponent<Jump>();
    }

    void Update()
    {
        //TODO
        GlobalPosition = new Vector3(transform.position.x, transform.position.y - Z + 0.5f, Z);

        velocity = (transform.position - _lastPosition) / Time.deltaTime;
        this._lastPosition = transform.position;

        if (velocity.sqrMagnitude > 0 + movingBuffer)
            isMoving = true;
        else
            isMoving = false;

        //how to tell if facing right? If last vel.x was positive
        if (velocity.x > 0)
            rightFacing = true;
        else if (velocity.x < 0)
            rightFacing = false;


        //do something with _jump here
        if (velocity.y > 0)
            upFacing = true;
        else if (velocity.y < 0)
            upFacing = false;


    }
}
