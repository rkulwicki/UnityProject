//This script is specifically for a player. It should take into acount the options
//that a player can choose during his/her turn.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerBattleButtons : MonoBehaviour
{
    public Button attackButton, moveButton, doNothingButton, ActionsButton, BackButtonActionsPanel, 
        OpenAttackListButton;

    private GameObject _actionsPanel;
    private GameObject _globalInputs;
    private GameObject _battleManager;
    private GameObject _player;
    private GameObject _attacksListScrollView;

    void Start()
    {
        _globalInputs = GameObject.FindGameObjectWithTag("GlobalInputs");
        _battleManager = GameObject.FindGameObjectWithTag("BattleManager");
        _actionsPanel = gameObject.transform.Find("PlayerBattleButtons")
            .gameObject.transform.Find("ActionsPanel").gameObject;
        _actionsPanel.SetActive(false);
        _attacksListScrollView = _actionsPanel.transform.Find("AttackListScrollView").gameObject;
        _attacksListScrollView.SetActive(false);
        _player = GameObject.FindGameObjectWithTag("Player");
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

    public void OpenActionsPanelOnClick()
    {
        _actionsPanel.SetActive(true);
    }

    public void CloseActionsPanelOnClick()
    {
        _actionsPanel.SetActive(false);
    }

    public void ToggleAttacksPanelOnClick()
    {
        if(_attacksListScrollView.activeSelf == true)
            _attacksListScrollView.SetActive(false);
        else
            _attacksListScrollView.SetActive(true);

        var stats = _player.GetComponent<PlayerStats>();
        Debug.Log(stats.attacks[0]); //do something with these
    }

    public void ToggleAttacksPanelOnClick(bool toggle)
    {
        if (toggle == true)
            _attacksListScrollView.SetActive(true);
        else
            _attacksListScrollView.SetActive(false);

        var stats = _player.GetComponent<PlayerStats>();
        Debug.Log(stats.attacks[0]); //do something with these
    }
}
