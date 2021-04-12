using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle
{
    public class BattlePlayerAnimationController : MonoBehaviour
    {
        public bool isHurt;
        public float hurtDuration;

        private bool _hurting;
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private Rigidbody2D _body2d;
        private BattlePlayer _battlePlayer;
        // Use this for initialization
        void Start()
        {
            _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            _animator = gameObject.GetComponent<Animator>();
            _body2d = gameObject.GetComponent<Rigidbody2D>();
            _battlePlayer = gameObject.GetComponent<BattlePlayer>();
            isHurt = false;
            _hurting = false;
        }

        // Update is called once per frame
        void Update()
        {
            FlipSprite();

            if (isHurt)
            {
                isHurt = false;
                _hurting = true;
                StartCoroutine(HurtForTime(hurtDuration));
            }

            if (!_hurting)
            {
                //facing right
                if (_spriteRenderer.flipX == true)
                {
                    if (!_battlePlayer.grounded)
                        JumpFront();
                    else if (Math.Abs(_body2d.velocity.x) > 0.1f)
                        WalkFront();
                    else
                        IdleFront();
                }
                //facing left
                else
                {
                    if (!_battlePlayer.grounded)
                        JumpFront();
                    else if (Math.Abs(_body2d.velocity.x) > 0.1f)
                        WalkFront();
                    else
                        IdleFront();
                }
            }
        }

        /// <summary>
        /// Assumes flipX == true faces right (meaning, default sprite view is left)
        /// </summary>
        private void FlipSprite()
        {
            if(_body2d.velocity.x > 0.05f)
            {
                _spriteRenderer.flipX = true;
            }
            else if(_body2d.velocity.x < -0.05f)
            {
                _spriteRenderer.flipX = false;
            }
        }

        public IEnumerator HurtForTime(float time)
        {
            HurtFront();
            yield return new WaitForSeconds(time);
            _hurting = false;
        }

        private void IdleFront()
        {
            _animator.SetInteger("AnimatorState", 0);
        }

        private void WalkFront()
        {
            _animator.SetInteger("AnimatorState", 2);
        }

        private void JumpFront()
        {
            _animator.SetInteger("AnimatorState", 4);
        }

        private void HurtFront()
        {
            _animator.SetInteger("AnimatorState", 6);
        }
    }
}