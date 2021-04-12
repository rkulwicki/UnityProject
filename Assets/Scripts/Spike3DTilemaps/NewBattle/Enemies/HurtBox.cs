using System.Collections;
using UnityEngine;
using static Globals;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle.Enemies
{
    /// <summary>
    /// Attach to a HurtBox gameobject, which is a child of the enemy object.
    /// Box that hurts player.
    /// </summary>
    public class HurtBox : MonoBehaviour
    {
        public int damageToPlayer;

        private BoxCollider2D _hurtBox;
        private BattlePlayer _battlePlayer;

        // Use this for initialization
        void Start()
        {
            _hurtBox = gameObject.GetComponent<BoxCollider2D>();
            _battlePlayer = GameObject.FindGameObjectWithTag(BattlePlayerTag).GetComponent<BattlePlayer>();
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.gameObject.tag == BattlePlayerTag)
            {
                _battlePlayer.health -= damageToPlayer;
            }
        }

    }
}