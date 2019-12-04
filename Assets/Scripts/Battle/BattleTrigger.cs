using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTrigger : MonoBehaviour
{

    private GameObject BattleManager;

    void Start()
    {
        BattleManager = GameObject.FindGameObjectWithTag("BattleManager");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            BattleManager.GetComponent<BattleManager>().isBattle = true;
        }
    }

}
