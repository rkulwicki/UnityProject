using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class AttackListButton : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private AttackBadge _attackBadge;

    private BattleManager _battleManager;
    private void Start()
    {
        _battleManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
    }

    public void SetText(string textString)
    {
        _text.text = textString;
    }

    public void SetAttackBadge(AttackBadge attackBadge)
    {
        _attackBadge = attackBadge;
    }

    public void OnClick()
    {
        _battleManager.chosenAttack = _attackBadge;
        _battleManager.attackButtonClicked = true;
    }
}