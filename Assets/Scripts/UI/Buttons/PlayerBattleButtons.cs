//This script is specifically for a player. It should take into acount the options
//that a player can choose during his/her turn.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBattleButtons : MonoBehaviour
{
    public Button attackButton, moveButton, doNothingButton;

    private GameObject _globalInputs;
    private GameObject _battleManager;
    void Start()
    {
        _globalInputs = GameObject.FindGameObjectWithTag("GlobalInputs");
        _battleManager = GameObject.FindGameObjectWithTag("BattleManager");
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
    public void DoNothingButtonOnClick()
    {
        _battleManager.GetComponent<BattleManager>().EndPlayerTurn();
    }

    public void ActionsButtonOpenOnClick()
    {
    }
}
