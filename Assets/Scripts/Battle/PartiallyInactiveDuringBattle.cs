using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartiallyInactiveDuringBattle : MonoBehaviour
{
    public GameObject _battleManager;

    void Start()
    {
        _battleManager = GameObject.FindGameObjectWithTag("BattleManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(_battleManager.GetComponent<BattleManager>().state != BattleState.INACTIVE)
        {
            foreach(var col in gameObject.GetComponentsInChildren<BoxCollider2D>())
            {
                col.enabled = false;
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            //gameObject.get<BoxCollider2D>().enabled = false;
        }
        else
        {
            foreach (var col in gameObject.GetComponentsInChildren<BoxCollider2D>())
            {
                col.enabled = true;
            }
            gameObject.GetComponent<SpriteRenderer>().enabled = true;

        }
    }
}
