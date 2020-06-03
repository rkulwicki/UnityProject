using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{

    private Animator _animator;

    private SpriteRenderer _spriteRenderer;
    private Jump _jump;
    private PlayerMove _playerMove;
    private PlayerStats _playerStats;

    public int test;

    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _animator = gameObject.GetComponent<Animator>();
        _jump = gameObject.GetComponent<Jump>();
        _playerMove = gameObject.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        //listen for change
        if (test == 1)
            IdleFrontLeft();
        if (test == 2)
            WalkFrontLeft();
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

    private void JumpRight()
    {
        _spriteRenderer.flipX = true;
        _animator.SetInteger("AnimatorState", 4);
    }

    private void JumpLeft()
    {
        _spriteRenderer.flipX = false;
        _animator.SetInteger("AnimatorState", 4);
    }

}
