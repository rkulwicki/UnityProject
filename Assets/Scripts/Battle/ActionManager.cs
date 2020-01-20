using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour
{

    public BattleChoicesEnum battleChoices;

    private void Start()
    {
        //assign to global battle choice attached to managers
        battleChoices = gameObject.GetComponent<BattleChoices>().battleChoice;
    }

    private void Update()
    {
        battleChoices = gameObject.GetComponent<BattleChoices>().battleChoice;
    }

    public void AttackChoice()
    {
        
    }
}
