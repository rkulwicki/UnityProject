using UnityEngine;
using static Globals;
using static BattleInitiator;

public class BattleArenaTrigger : MonoBehaviour
{
    public BattleArenaName battleArenaName;
    public EnemyName[] battleEnemies;
    public Vector2Int[] battleEnemySpawnPoints;
    public Vector2Int playerSpawnPoint;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("triggered");
        if(col.gameObject.tag == PlayerTag)
        {
            InitiateBattle(battleArenaName, battleEnemies, battleEnemySpawnPoints, playerSpawnPoint);
        }
    }
}
