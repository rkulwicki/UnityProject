using UnityEngine;
using System.Collections;
using static Globals;
public static class PlayerStaticFunctions
{
    public static GameObject GetOrInstantiatePlayer(Vector3 position, Quaternion rotation)
    {
        if (GameObject.FindGameObjectWithTag(PlayerTag) != null)
        {
            return GameObject.FindGameObjectWithTag(PlayerTag);
        }
        else
        {
            var playerPrefab = Resources.Load(PlayerPrefabPath) as GameObject;
            var player = Object.Instantiate(playerPrefab, position, rotation);
            return player;
        }
    }

    public static void SuspendPlayerMovement(bool isSuspended)
    {
        var player = GetOrInstantiatePlayer(new Vector3(0,0,0), new Quaternion());
        if (isSuspended)
        {
            player.GetComponent<Pseudo3DPlayer>().isPlayerMovementSuspended = true;
            player.GetComponent<Gravity>().enabled = false;
        }
        else
        {
            player.GetComponent<Pseudo3DPlayer>().isPlayerMovementSuspended = false;
            player.GetComponent<Gravity>().enabled = true;
        }
    }
}
