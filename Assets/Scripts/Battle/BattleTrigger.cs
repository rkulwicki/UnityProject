//Name: BattleTrigger
//Desc: Relies on BattleManager
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

    private void Start()
    {
        _tagsGlobal = gameObject.AddComponent<TagsGlobal>();
        _tagsToIgnore = _tagsGlobal.tagsToIgnoreInBattle;
        enemyStats = gameObject.GetComponentInParent<EnemyStats>(); //get stats from 
        //string[] tagsToIgnore = global list of tags to ignore.
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //find BattleManager
            battleManager = GameObject.FindGameObjectWithTag("BattleManager");
            battleManager.GetComponent<BattleManager>().state = BattleState.START; //set to start

            //give BattleManager the Actors in the battle
            playersInvolved = GetPlayerAndPartner();
            battleManager.GetComponent<BattleManager>().playersInvolved = playersInvolved;


            enemiesInvolved = GetObjectsWithTagInRange("Enemy", gameObject.transform.position, enemyStats.enemyAccompanyRadius);

            //objectsNotInvolved = GetObjectsWithTagInRange(_tagsToIgnore, gameObject.transform.position, enemyStats.enemyAccompanyRadius);
            //todo: GetEnemiesWithinRange 

            battleManager.GetComponent<BattleManager>().enemiesInvolved = enemiesInvolved;
        }
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

    [Description("Gets objects within a certain units of a point.")]
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

    [Description("Gets objects within a certain units of a point.")]
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
}
