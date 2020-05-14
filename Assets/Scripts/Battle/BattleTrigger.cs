//Name: BattleTrigger
//Desc: Relies on BattleManager
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{
    public float timeToWaitAfterTriggered;

    public GameObject battleManager;

    public GameObject[] enemiesInvolved;
    public GameObject[] playersInvolved;
    public GameObject[] objectsNotInvolved;

    public float enemyAccompanyRadius;

    public EnemyStats enemyStats;

    public bool triggered, actorsAreStoped; 

    private GameObject _player;
    private GameObject _partner;
    private TagsGlobal _tagsGlobal;
    private string[] _tagsToIgnore;
    private GameObject _playerBattleActionHud;
    private GameObject _tileManager;

    protected EnemyMoveAI moveScript;
    //isBattle

    private void Start()
    {
        _tagsGlobal = gameObject.AddComponent<TagsGlobal>();
        _tagsToIgnore = _tagsGlobal.tagsToIgnoreInBattle;
        enemyStats = gameObject.GetComponentInParent<EnemyStats>(); //get stats from 
        _tileManager = GameObject.FindGameObjectWithTag("TileManager");

        moveScript = gameObject.GetComponentInParent<EnemyMoveAI>();

        battleManager = GameObject.FindGameObjectWithTag("BattleManager");

    }

    private void Update()
    {
        //move on trigger enter to here
        if (triggered) //stop actors movement
        {
            moveScript.enabled = false;

            battleManager.GetComponent<BattleManager>().state = BattleState.BEFORESTART; //set to start

            //wait for time for a time so it doesn't mess up the battle manager for some reason.
            StartCoroutine(Wait(timeToWaitAfterTriggered));
            triggered = false;
        }

        if (actorsAreStoped) //set up battle manager
        {
            enemiesInvolved = GetObjectsInTiles(new string[1] { "Enemy" }, enemyStats.GetRelativeBattleArea()); //here is the problem. No "Enemies Involved"
            


            //============================test
            //enemiesInvolved = new GameObject[1] { this.gameObject.transform.parent.gameObject };
            
            
            
            
            
            var thisEnemy = gameObject.transform.parent.gameObject;
            battleManager.GetComponent<BattleManager>().initiatedEnemy = thisEnemy; //this is the initiated enemy.
            playersInvolved = GetPlayerAndPartner();
            battleManager.GetComponent<BattleManager>().playersInvolved = playersInvolved;

            battleManager.GetComponent<BattleManager>().enemiesInvolved = enemiesInvolved;

            battleManager.GetComponent<BattleManager>().state = BattleState.START; //set to start

            actorsAreStoped = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") triggered = true;
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        actorsAreStoped = true;
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
    /// <summary>
    /// Freeze movement of objects and rounds their position to nearest Int.
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
    public GameObject[] StopActorsMovement(string[] tags, Vector3Int[] tiles)
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
                foreach (var tile in tiles)
                {
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
