using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBattleActionHudScript : MonoBehaviour
{

    public Button AttackButton, MoveButton;

    // Use this for initialization
    void Start()
    {
        AttackButton.onClick.AddListener(TaskOnClickAttack);
        MoveButton.onClick.AddListener(TaskOnClickMove);
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
