﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerAttacksListButtons : MonoBehaviour
{
    public float buttonBuffer;

    [SerializeField]
    private GameObject _buttonTemplate;

    private PlayerStats _playerStats;

    void Start()
    {
        float posYOffset = buttonBuffer + _buttonTemplate.GetComponent<RectTransform>().rect.height;

        _playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        if(_playerStats.attacks != null)
        {
            var pos = 0f; //_buttonTemplate.GetComponent<RectTransform>().rect.position.y;
            _buttonTemplate.SetActive(false);
            //TODO!!!!!
            //set button's on-click action to be AttackBadge's action.

            foreach (var attack in _playerStats.attacks)
            {
                GameObject button = Instantiate(_buttonTemplate) as GameObject;
                button.SetActive(true);

                button.GetComponent<AttackListButton>().SetText(attack.badgeName);
                button.GetComponent<AttackListButton>().SetAttackBadge(attack);

                button.transform.SetParent(_buttonTemplate.transform.parent, false);
                button.transform.position += new Vector3(0, pos, 0);
                pos = pos - posYOffset;

                button.GetComponent<Button>().onClick.AddListener(new UnityAction(() =>
                    button.GetComponent<AttackListButton>().OnClick()));
                button.SetActive(true);
            };

        }
    }

    void TaskOnClick()
    {
        Debug.Log("afsdfa");
    }
}
