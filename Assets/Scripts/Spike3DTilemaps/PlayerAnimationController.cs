using UnityEngine;
using System.Collections;
using System;

public class PlayerAnimationController : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private DirectionAndMovementInterface _directionAndMovementInterface;
    private Vector3 _v3;
    private XYDirection _xyDir;
    private float _animationBuffer = 0.25f;
    private float _jumpAnimationBuffer = 0.1f;

    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
        _directionAndMovementInterface = transform.parent.GetComponent<DirectionAndMovementInterface>();
    }

    void Update()
    {
        _v3 = _directionAndMovementInterface.pseudo3DVelocity;
        _xyDir = _directionAndMovementInterface.direction;

        if(_xyDir == XYDirection.XPosYPos)
        {
            if(Math.Abs(_v3.z) > 0 + _jumpAnimationBuffer)
            {
                JumpBackRight();
            }
            else if(Math.Abs(_v3.x) > 0 + _animationBuffer | Math.Abs(_v3.y) > 0 + _animationBuffer)
            {
                WalkBackRight();
            }
            else
            {
                IdleBackRight();
            }
        }
        else if(_xyDir == XYDirection.XPosYNeg)
        {
            if (Math.Abs(_v3.z) > 0 + _jumpAnimationBuffer)
            {
                JumpFrontRight();
            }
            else if (Math.Abs(_v3.x) > 0 + _animationBuffer | Math.Abs(_v3.y) > 0 + _animationBuffer)
            {
                WalkFrontRight();
            }
            else
            {
                IdleFrontRight();
            }
        }
        else if (_xyDir == XYDirection.XNegYPos)
        {
            if (Math.Abs(_v3.z) > 0 + _jumpAnimationBuffer)
            {
                JumpBackLeft();
            }
            else if (Math.Abs(_v3.x) > 0 + _animationBuffer | Math.Abs(_v3.y) > 0 + _animationBuffer)
            {
                WalkBackLeft();
            }
            else
            {
                IdleBackLeft();
            }
        }
        else //_xyDir == XYDirection.XNegYNeg
        {
            if (Math.Abs(_v3.z) > 0 + _jumpAnimationBuffer)
            {
                JumpFrontLeft();
            }
            else if (Math.Abs(_v3.x) > 0 + _animationBuffer | Math.Abs(_v3.y) > 0 + _animationBuffer)
            {
                WalkFrontLeft();
            }
            else
            {
                IdleFrontLeft();
            }
        }


        //if (_v3.x > 0 && _v3.y > 0)
        //    WalkFrontRight();
        //else if (_v3.x < 0)
        //    WalkFrontLeft();
        //else
        //    IdleFrontRight();
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
