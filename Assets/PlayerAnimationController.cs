using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private Jump _jump;
    private PlayerMove _playerMove;
    private PlayerStats _playerStats;

    void Start()
    {
        _jump = gameObject.GetComponent<Jump>();
        _playerMove = gameObject.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //check for movement

        //check for jump

        //check for hurt
    }

    private void IdleFrontLeft()
    {

    }

    private void IdleFrontRight()
    {

    }

    private void IdleBackLeft()
    {

    }

    private void IdleBackRight()
    {

    }

    private void WalkFrontLeft()
    {

    }

    private void WalkFrontRight()
    {

    }


}
