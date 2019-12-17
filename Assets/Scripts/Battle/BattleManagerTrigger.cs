//Name: BattleTrigger
//Desc: Relies on BattleManager
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagerTrigger : MonoBehaviour
{
    public GameObject _battleManager;
    private GameObject _player;

    public GameObject[] enemiesInvolved;
    public GameObject[] playersInvolved;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            //find BattleManager
            _battleManager = GameObject.FindGameObjectWithTag("BattleManager");
            _battleManager.GetComponent<BattleManager>().state = BattleState.START;

        }
    }
}
