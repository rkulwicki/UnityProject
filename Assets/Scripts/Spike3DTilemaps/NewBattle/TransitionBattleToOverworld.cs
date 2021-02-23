using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Globals;
using static PlayerStaticFunctions;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle
{
    public static class TransitionBattleToOverworld
    {
        public static void TransitionToOverworld()
        {
            var persistentData = GameObject.FindGameObjectWithTag(PersistentDataTag);
            //load scene, then set player data
            SceneManager.LoadScene(persistentData.GetComponent<PersistentData>().previousScene.ToString());
            var player = GetOrInstantiatePlayer(new Vector3(0,0,0), new Quaternion());
            player.GetComponent<Pseudo3DPlayer>().pseudo3DPosition = persistentData.GetComponent<PersistentData>().playerSpawnPointInOverworld;
        }
    }
}