using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Globals;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle
{
    public static class TransitionOverworldToBattle
    {
        public static void TransitionToBattle()
        {
            //save all player info from the overworld, then load scene
            var persistentData = GameObject.FindGameObjectWithTag(PersistentDataTag);
            persistentData.GetComponent<PersistentData>().previousScene = (SceneNames)Enum.Parse(typeof(SceneNames), SceneManager.GetActiveScene().name);
            var player = GameObject.FindGameObjectWithTag(PlayerTag);
            persistentData.GetComponent<PersistentData>().playerSpawnPointInOverworld = player.GetComponent<Pseudo3DPlayer>().pseudo3DPosition;
            SceneManager.LoadScene(persistentData.GetComponent<PersistentData>().battleScene.ToString());
        }
    }
}