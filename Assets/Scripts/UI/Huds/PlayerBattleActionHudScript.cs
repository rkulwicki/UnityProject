//This script is specifically for a player. It should take into acount the options
//that a player can choose during his/her turn.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBattleActionHudScript : MonoBehaviour
{
    public Button attackButton, moveButton;
    public BattleChoicesEnum battleChoice;
    //TODO: add buttons for other options during battle like:
    //"Run Away", "Do Nothing", "Change Partner", etc.

    void Start()
    {
        attackButton.onClick.AddListener(TaskOnClickAttack);
        moveButton.onClick.AddListener(TaskOnClickMove);
    }

    void TaskOnClickAttack()
    {
        Debug.Log("You chose attack. Very cool.");
    }

    void TaskOnClickMove()
    {
        Debug.Log("You chose move. This sucks I hate it.");
    }
}
