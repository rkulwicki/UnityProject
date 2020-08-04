using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFollowPlayer : MonoBehaviour
{
    private GameObject _p;
    // Start is called before the first frame update
    void Start()
    {
        _p = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _p.GetComponent<MovementInfo>().GlobalPosition;
    }
}
