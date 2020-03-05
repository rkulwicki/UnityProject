using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyStats : ActorStats
{
    public string enemyDescription;
    public int experienceReward;
    public float enemyAccompanyRadius;
    public int enemyBattleRadius; //as of now, the battleRadius should be bigger than enemyAccompanyRadius
    public int enemySightRadius;
    public Tile battleBoundaryTile;
}
