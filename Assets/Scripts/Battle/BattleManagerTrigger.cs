//Name: BattleTrigger
//Desc: Relies on BattleManager
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BattleManagerTrigger : MonoBehaviour
{
    public GameObject battleManager;
    private GameObject _player;
    private GameObject _partner;

    public GameObject[] enemiesInvolved;
    public GameObject[] playersInvolved;

    public float enemyAccompanyRadius;

    public EnemyStats enemyStats;

    private void Start()
    {
        enemyStats = gameObject.GetComponentInParent<EnemyStats>(); //get stats from 
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

            //todo: GetEnemiesWithinRange 
            battleManager.GetComponent<BattleManager>().enemiesInvolved = enemiesInvolved;


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

        var totalEnemiesInvolved = GameObject.FindGameObjectsWithTag(tag); //gets all enemies but we only want ones so far away

        GameObject[] enemiesInvolved;
        List<GameObject> enemiesList = new List<GameObject>();

        for (int i = 0; i < totalEnemiesInvolved.Length; i++)
        {
            float distance = Vector3.Distance(center, totalEnemiesInvolved[i].transform.position);
            if (distance <= radius)
            {
                enemiesList.Add(totalEnemiesInvolved[i]);
            }
        }
        enemiesInvolved = enemiesList.ToArray();
        return enemiesInvolved;
        
    }
}
