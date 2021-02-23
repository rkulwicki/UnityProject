using System.Collections;
using UnityEngine;
using static Assets.Scripts.Spike3DTilemaps.NewBattle.TransitionOverworldToBattle;
using static Globals;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle
{
    public class TESTTransitionToBattle : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.gameObject.tag == PlayerTag)
            {
    
                //make persistent data start location set to where it needs to be
                TransitionToBattle();
            }
        }
    }
}