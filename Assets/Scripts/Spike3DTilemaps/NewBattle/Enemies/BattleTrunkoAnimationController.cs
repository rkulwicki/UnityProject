using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle.Enemies
{
    public class BattleTrunkoAnimationController : MonoBehaviour
    {
        public bool isHurt;
        public float hurtDuration;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Rigidbody2D _body2d;
        private TrunkoAI _trunkoAI;
        // Use this for initialization
        void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _animator = gameObject.GetComponent<Animator>();
            _body2d = gameObject.GetComponent<Rigidbody2D>();
            _trunkoAI = gameObject.GetComponent<TrunkoAI>();

            isHurt = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_trunkoAI.movingRight)
                WalkFrontRight();
            else
                WalkFrontLeft();

        }

        public IEnumerator HurtForTime(float time)
        {
            HurtFront();
            yield return new WaitForSeconds(time);
            isHurt = false;
        }

        private void IdleFront()
        {
            _animator.SetInteger("AnimationState", 0);
        }

        private void WalkFrontRight()
        {
            _spriteRenderer.flipX = true;
            _animator.SetInteger("AnimationState", 1);
        }

        private void WalkFrontLeft()
        {
            _spriteRenderer.flipX = false;
            _animator.SetInteger("AnimationState", 1);
        }

        private void HurtFront()
        {
            _animator.SetInteger("AnimationState", 2);
        }
    }
}
