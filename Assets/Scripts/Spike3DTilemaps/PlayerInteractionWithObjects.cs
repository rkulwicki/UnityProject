using UnityEngine;
using System.Collections;
using static Globals;
public class PlayerInteractionWithObjects : MonoBehaviour
{
    public GameObject InteractiveObject;
    public bool isActive;

    private GameObject _eMark;
    private GameObject _tempEMark;

    private void Start()
    {
        _eMark = Resources.Load(EMarkPath) as GameObject;
    }
    private void Update()
    {
        if (InteractiveObject != null && _tempEMark == null && !isActive) //spawn an exclamation mark
        {
            _tempEMark = Instantiate(_eMark, this.transform.position + new Vector3(0,2,0), Quaternion.identity);
            _tempEMark.transform.parent = gameObject.transform;
        }
        else if (InteractiveObject == null || 
            (InteractiveObject != null && isActive))
        {
            Destroy(_tempEMark);
        }
    }
}
