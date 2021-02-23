using UnityEngine;
using static Globals;
using UnityEngine.SceneManagement;

public static class BattleInitiator
{
    /// <summary>
    /// Changes the scene from where we currently are to a battle determined by the name
    /// of the battle arena (Global enum) and the player and enemies.
    /// </summary>
    public static void InitiateBattle(BattleArenaName battleArenaName, EnemyName[] battleEnemies, Vector2Int[] battleEnemySpawnPoints,
        Vector2Int playerSpawnPoint)
    {
        SetBattlePersistentData(battleArenaName, battleEnemies, battleEnemySpawnPoints, playerSpawnPoint);
        LoadBattleScene();
    }

    private static void SetBattlePersistentData(BattleArenaName battleArenaName, EnemyName[] battleEnemies, Vector2Int[] battleEnemySpawnPoints,
        Vector2Int playerSpawnPoint)
    {
        PersistentData.data.battleArenaName = battleArenaName;
        PersistentData.data.battleEnemies = battleEnemies;
        PersistentData.data.battleEnemiesSpawnPoints = battleEnemySpawnPoints;
        PersistentData.data.playerSpawnPointInBattle = playerSpawnPoint;
    }

    private static void LoadBattleScene()
    {
        SceneManager.LoadScene(SceneNames.BattleScene.ToString());
    }
}
