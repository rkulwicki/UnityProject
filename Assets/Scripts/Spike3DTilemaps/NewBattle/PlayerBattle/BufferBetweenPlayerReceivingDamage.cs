using System.Collections;
using UnityEngine;
using static Globals;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle.PlayerBattle
{
    public class BufferBetweenPlayerReceivingDamage : MonoBehaviour
    {
        private GameObject _battlePlayerGameObject;
        private BattlePlayer _battlePlayer;
        private BattlePlayerAnimationController _battlePlayerAnimationController;
        private int _currentHealth;
        // Use this for initialization
        void Start()
        {
            _battlePlayerGameObject = GameObject.FindGameObjectWithTag(BattlePlayerTag);
            _battlePlayer = _battlePlayerGameObject.GetComponent<BattlePlayer>();
            _battlePlayerAnimationController = _battlePlayerGameObject.GetComponent<BattlePlayerAnimationController>();
            _currentHealth = PersistentData.data.health;
        }

        // Update is called once per frame
        void Update()
        {
            if (_currentHealth != _battlePlayer.health)
            {
                //1. Change animation hurt
                _battlePlayerAnimationController.isHurt = true;

                //2. Flash white

                //3. Invincible for like, idk, a second or whatever


                //4. Set current health
                _currentHealth = _battlePlayer.health;
            }

        }
    }
}
