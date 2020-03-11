//Name: BattleTrigger
//Desc: Relies on BattleManager
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    public GameObject battleManager;

    public GameObject[] enemiesInvolved;
    public GameObject[] playersInvolved;
    public GameObject[] objectsNotInvolved;

    public float enemyAccompanyRadius;

    public EnemyStats enemyStats;

    private GameObject _player;
    private GameObject _partner;
    private TagsGlobal _tagsGlobal;
    private string[] _tagsToIgnore;
    private GameObject _playerBattleActionHud;
    private GameObject _tileManager;
    private void Start()
    {
        _tagsGlobal = gameObject.AddComponent<TagsGlobal>();
        _tagsToIgnore = _tagsGlobal.tagsToIgnoreInBattle;
        enemyStats = gameObject.GetComponentInParent<EnemyStats>(); //get stats from 
        _tileManager = GameObject.FindGameObjectWithTag("TileManager");
        //_playerBattleActionHud = ;
        //string[] tagsToIgnore = global list of tags to ignore.

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            // maybe todo? vvvvv
            //_playerBattleActionHud.GetComponent<PlayerBattleActionsHudScript>().StartState();

            SetUpBattleManager();
            //enemiesInvolved = GetObjectsWithTagInRange("Enemy", gameObject.transform.position, enemyStats.enemyAccompanyRadius);
            //TODO: find the enemy if it is in enemyStats.battleArea
            //var tiles = _tileManager.GetComponent<TileManager>().GenerateBoundaryFromArea(enemyStats.battleArea);
            enemiesInvolved = GetObjectsInTiles(new string[1]{"Enemy"}, enemyStats.battleArea);

            battleManager.GetComponent<BattleManager>().enemiesInvolved = enemiesInvolved;
        }
    }

    private void SetUpBattleManager()
    {
        battleManager = GameObject.FindGameObjectWithTag("BattleManager");
        battleManager.GetComponent<BattleManager>().state = BattleState.START; //set to start
        battleManager.GetComponent<BattleManager>().initiatedEnemy = gameObject.transform.parent.gameObject; //this is the initiated enemy.
        playersInvolved = GetPlayerAndPartner();
        battleManager.GetComponent<BattleManager>().playersInvolved = playersInvolved;
    }

    private void ObjectsSetActive(GameObject[] objs, bool value)
    {
        foreach(var obj in objs)
        {
            obj.SetActive(value);
        }
    }

    private GameObject[] GetPlayerAndPartner()    
    {
        GameObject[] playersAndPartner;
        if (GameObject.FindGameObjectWithTag("Partner") != null)
        {
            playersAndPartner = new GameObject[2];
            var partner = GameObject.FindGameObjectWithTag("Partner");
            playersAndPartner[1] = partner;
        } //else partnerExists = false;
        else
        {
            playersAndPartner = new GameObject[1];
        }

        var player = GameObject.FindGameObjectWithTag("Player"); //get player
        playersAndPartner[0] = player;

        return playersAndPartner;
    }

    /// <summary>
    /// Gets objects within a certain units of a point.
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
    public GameObject[] GetObjectsWithTagInRange(string tag, Vector3 center, float radius)
    {
        var total = GameObject.FindGameObjectsWithTag(tag); //gets all enemies but we only want ones so far away
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < total.Length; i++)
        {
            float distance = Vector3.Distance(center, total[i].transform.position);
            if (distance <= radius)
            {
                list.Add(total[i]);
            }
        }
        return list.ToArray();  
    }

    /// <summary>
    /// Gets objects within a certain units of a point.
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
    public GameObject[] GetObjectsWithTagInRange(string[] tags, Vector3 center, float radius)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var tag in tags)
        {
            var allWithTag = GameObject.FindGameObjectsWithTag(tag); //gets all objects but we only want ones so far away

            for (int i = 0; i < allWithTag.Length; i++)
            {
                float distance = Vector3.Distance(center, allWithTag[i].transform.position);
                if (distance <= radius)
                {
                    list.Add(allWithTag[i]);
                }
            }
        }
        return list.ToArray();
    }

    /// <summary>
    /// Gets an array of GameObjects if they contain a tag and their position is in the list of positions.
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
    public GameObject[] GetObjectsInTiles(string[] tags, Vector3Int[] tiles)
    {
        List<GameObject> list = new List<GameObject>();
        foreach (var tag in tags)
        {
            var allWithTag = GameObject.FindGameObjectsWithTag(tag); //gets all objects but we only want ones so far away

            for (int i = 0; i < allWithTag.Length; i++)
            {
                var posRounded = new Vector3Int(Convert.ToInt32(allWithTag[i].transform.position.x),
                                                Convert.ToInt32(allWithTag[i].transform.position.y),
                                                Convert.ToInt32(allWithTag[i].transform.position.z));
                foreach (var tile in tiles) {
                    if (posRounded == tile)
                    {
                        list.Add(allWithTag[i]);
                    }
                }
            }
        }
        return list.ToArray();
    }
}
