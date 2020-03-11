using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleActionsHudScript : MonoBehaviour
{

    public GameObject playerBattleButtons;
    public GameObject _actionsPanel;

    void Start()
    {
        _actionsPanel = gameObject.transform.Find("PlayerBattleButtons")
            .gameObject.transform.Find("ActionsPanel").gameObject;
        _actionsPanel.SetActive(false);
    }
    public void StartState()
    {
        _actionsPanel.SetActive(false);
    }
}
