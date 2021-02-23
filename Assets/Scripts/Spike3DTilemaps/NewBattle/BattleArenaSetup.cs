using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using static Globals;
using static BattleRepository;

public class BattleArenaSetup : MonoBehaviour
{
    public BattleArenaName battleArenaName;

    public Vector2Int size;
    public TileBase tile;
    public EnemyName[] battleEnemies;
    public Vector2Int[] battleEnemySpawnPoints;
    public Vector2 playerSpawnPoint;

    private GameObject _battleTilemap;
    private GameObject _mainCamera;
    private GameObject _battlePlayer;

    // Use this for initialization
    private void Start()
    {
        battleArenaName = PersistentData.data.battleArenaName;
        battleEnemies = PersistentData.data.battleEnemies;
        battleEnemySpawnPoints = PersistentData.data.battleEnemiesSpawnPoints;
        playerSpawnPoint = PersistentData.data.playerSpawnPointInBattle;

        var ba = GetBattleInfoFromBattleArenaName(battleArenaName);
        tile = ba.tile;
        size = ba.size;

        _battleTilemap = GameObject.FindGameObjectWithTag(BattleTilemapTag);
        _mainCamera = GameObject.FindGameObjectWithTag(MainCameraTag);

        //PersistentData.data.battleArena

        SetupBattleBounds(size.x, size.y);
        AdjustCamera(size.x, size.y);
        SetRandomBlocks();
        SpawnPlayer();
        SpawnEnemies();
        BeforeBattleAnimation();
    }

    public void SetupBattleBounds(int x, int y)
    {
        x++;
        y++;
        for (int i = 0; i < x + 1; i++)
        {
            for (int j = 0; j < y + 1; j++)
            {
                if (i == 0 || i == x || j == 0 || j == y) //if we are at the boundary
                    _battleTilemap.GetComponent<Tilemap>().SetTile(new Vector3Int(i, j, 0), tile);
            }
        }

    }

    public void AdjustCamera(int x, int y)
    {
        x = x + 2;
        y = y + 2;
        if (x < y)
            _mainCamera.GetComponent<Camera>().orthographicSize = (x) / 2;
        else
            _mainCamera.GetComponent<Camera>().orthographicSize = (y) / 2;

        _mainCamera.transform.position = new Vector3(x / 2, y / 2, _mainCamera.transform.position.z);
    }

    public void SetRandomBlocks()
    {
        //todo
    }

    public void SpawnPlayer()
    {
        //todo playerSpawnPoint
        _battlePlayer = Resources.Load("Prefabs/BattlePlayer") as GameObject;
        var go = Instantiate(_battlePlayer);
        go.transform.position = new Vector3(playerSpawnPoint.x, playerSpawnPoint.y, 0);
    }

    public void SpawnEnemies()
    {
        //todo
    }

    public void BeforeBattleAnimation()
    {
        //todo
    }
}
