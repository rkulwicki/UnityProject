using UnityEngine;
using UnityEngine.UI;

public class AttackListButton : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    private AttackBadge _attackBadge; 

    public void SetText(string textString)
    {
        _text.text = textString;
    }

    public void OnClick()
    {
        //
    }
}