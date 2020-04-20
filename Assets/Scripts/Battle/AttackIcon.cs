using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AttackIcon : MonoBehaviour
{

    public GameObject attackIconPrefab;
    //todo set int to prefab
    

    public GameObject Spawn(Vector3 loc, int num)
    {
        var icon = Instantiate(this.attackIconPrefab);
        icon.transform.position = loc;

        var textObj = icon.transform.GetChild(0).GetChild(0).gameObject;
        var text = textObj.GetComponent<Text>();
        text.text = num.ToString();

        return icon;
    }
}
