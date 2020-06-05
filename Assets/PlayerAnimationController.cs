using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    public float movementBuffer = 0.01f;

    private Rigidbody2D _rigidBody2d;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Jump _jump;
    private PlayerMove _playerMove;
    private PlayerStats _playerStats;
    private MovementInfo _mi;

    public int test;

    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
        _jump = gameObject.GetComponent<Jump>();
        _playerMove = gameObject.GetComponent<PlayerMove>();
        _rigidBody2d = gameObject.GetComponent<Rigidbody2D>();
        _mi = gameObject.GetComponent<MovementInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        //this is in order of importance.

        //jumping
        if (_jump.jumping && _mi.rightFacing && !_mi.upFacing)
            JumpFrontRight();
        else if(_jump.jumping && !_mi.rightFacing && !_mi.upFacing)
            JumpFrontLeft();
        //jumping back
        else if (_jump.jumping && _mi.rightFacing && _mi.upFacing)
            JumpBackRight();
        else if (_jump.jumping && !_mi.rightFacing && _mi.upFacing)
            JumpBackLeft();

        //walking
        else if (_mi.isMoving && _mi.rightFacing && !_mi.upFacing)
            WalkFrontRight();
        else if (_mi.isMoving && !_mi.rightFacing && !_mi.upFacing)
            WalkFrontLeft();
        //walking back
        else if (_mi.isMoving && _mi.rightFacing && _mi.upFacing)
            WalkBackRight();
        else if (_mi.isMoving && !_mi.rightFacing && _mi.upFacing)
            WalkBackLeft();

        //idle
        else if (!_mi.isMoving && _mi.rightFacing && !_mi.upFacing)
            IdleFrontRight();
        else if (!_mi.isMoving && !_mi.rightFacing && !_mi.upFacing)
            IdleFrontLeft();
        //idle back
        else if (!_mi.isMoving && _mi.rightFacing && _mi.upFacing)
            IdleBackRight();
        else if (!_mi.isMoving && !_mi.rightFacing && _mi.upFacing)
            IdleBackLeft();

    }

    private void IdleFrontLeft()
    {
        _spriteRenderer.flipX = false;
        _animator.SetInteger("AnimatorState", 0);
    }

    private void IdleFrontRight()
    {
        _spriteRenderer.flipX = true;
        _animator.SetInteger("AnimatorState", 0);
    }

    private void IdleBackLeft()
    {
        _spriteRenderer.flipX = false;
        _animator.SetInteger("AnimatorState", 1);
    }

    private void IdleBackRight()
    {
        _spriteRenderer.flipX = true;
        _animator.SetInteger("AnimatorState", 1);
    }

    private void WalkFrontLeft()
    {
        _spriteRenderer.flipX = false;
        _animator.SetInteger("AnimatorState", 2);
    }

    private void WalkFrontRight()
    {
        _spriteRenderer.flipX = true;
        _animator.SetInteger("AnimatorState", 2);
    }

    private void WalkBackLeft()
    {
        _spriteRenderer.flipX = false;
        _animator.SetInteger("AnimatorState", 3);
    }

    private void WalkBackRight()
    {
        _spriteRenderer.flipX = true;
        _animator.SetInteger("AnimatorState", 3);
    }

    private void JumpFrontRight()
    {
        _spriteRenderer.flipX = true;
        _animator.SetInteger("AnimatorState", 4);
    }

    private void JumpFrontLeft()
    {
        _spriteRenderer.flipX = false;
        _animator.SetInteger("AnimatorState", 4);
    }

    private void JumpBackRight()
    {
        _spriteRenderer.flipX = true;
        _animator.SetInteger("AnimatorState", 5);
    }

    private void JumpBackLeft()
    {
        _spriteRenderer.flipX = false;
        _animator.SetInteger("AnimatorState", 5);
    }

}
