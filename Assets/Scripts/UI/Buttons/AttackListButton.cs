using UnityEngine;
using UnityEngine.UI;

public class AttackListButton : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    public void SetText(string textString)
    {
        _text.text = textString;
    }

    public void OnClick()
    {

    }
}