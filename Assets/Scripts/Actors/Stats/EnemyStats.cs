using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyStats : ActorStats
{
    public string enemyDescription;
    public int experienceReward;
    public float enemyAccompanyRadius;
    //public int enemyBattleRadius; //as of now, the battleRadius should be bigger than enemyAccompanyRadius
    public int enemySightRadius;
    public Tile battleBoundaryTile;
    public Vector3Int[] battleArea;
    public AttackBadge[] attacks;

    public Vector3Int[] GetRelativeBattleArea()
    {
        var center = new Vector3Int(Convert.ToInt32(gameObject.transform.position.x),
                                Convert.ToInt32(gameObject.transform.position.y),
                                Convert.ToInt32(gameObject.transform.position.z));
        var globalPos = new Vector3Int[battleArea.Length];
        for (int i = 0; i < battleArea.Length; i++)
        {
            globalPos[i] = battleArea[i] + center;
        }
        return globalPos;
    }
}
