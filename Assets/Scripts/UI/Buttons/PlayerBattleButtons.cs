//This script is specifically for a player. It should take into acount the options
//that a player can choose during his/her turn.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBattleButtons : MonoBehaviour
{
    public Button attackButton, moveButton;

    private GameObject _globalInputs;

    void Start()
    {
        _globalInputs = GameObject.FindGameObjectWithTag("GlobalInputs");
    }

    public void AttackButtonPressed()
    {
        _globalInputs.GetComponent<PlayerBattleGlobal>().AttackButton = true;
    }

    public void AttackButtonReleased()
    {
        _globalInputs.GetComponent<PlayerBattleGlobal>().AttackButton = false;
    }

    public void MoveButtonPressed()
    {
        _globalInputs.GetComponent<PlayerBattleGlobal>().MoveButton = true;
    }

    public void MoveButtonReleased()
    {
        _globalInputs.GetComponent<PlayerBattleGlobal>().MoveButton = false;

    }
}
