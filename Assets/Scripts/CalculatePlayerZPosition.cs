using UnityEngine;
using System.Collections;

public class CalculatePlayerZPosition : MonoBehaviour
{

    public float z;

    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
            z = _player.transform.position.y;
    }
}
