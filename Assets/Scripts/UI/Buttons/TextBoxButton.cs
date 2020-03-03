using UnityEngine;
using UnityEngine.UI;

public class TextBoxButton : MonoBehaviour
{

    public Button textBoxButton;

    //[injected]
    private GameObject _globalInputs;

    void Start()
    {
        _globalInputs = GameObject.FindGameObjectWithTag("GlobalInputs");
    }

    public void TextButtonPressed()
    {
        _globalInputs.GetComponent<TextBoxGlobal>().textBoxButton = true;
    }

    public void TextButtonReleased()
    {
        _globalInputs.GetComponent<TextBoxGlobal>().textBoxButton = false;
    }
}
