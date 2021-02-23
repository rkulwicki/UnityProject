using System.Collections;
using UnityEngine;
using static Globals;
using static Assets.Scripts.Spike3DTilemaps.NewBattle.TransitionBattleToOverworld;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle
{
    public class TESTTransitionOnEnterCollision : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.gameObject.tag == BattlePlayerTag)
            {
                TransitionToOverworld();
            }
        }
    }
}