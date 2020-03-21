using UnityEngine;
using System.Collections;

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
            var pos = _buttonTemplate.GetComponent<RectTransform>().rect.position.y;
            foreach (var attack in _playerStats.attacks)
            {
                GameObject button = Instantiate(_buttonTemplate) as GameObject;
                button.SetActive(true);

                button.GetComponent<AttackListButton>().SetText(attack);

                button.transform.SetParent(_buttonTemplate.transform.parent, false);
                button.transform.position += new Vector3(0, pos, 0);
                pos = pos - posYOffset;
            };

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
