using UnityEngine;
using System.Collections;
using static TestTilemapLogic;

public class TestShadow : MonoBehaviour
{
    public Vector3 shadowPseudo3DPos;
    private GameObject _player;

    private static string PlayerTag = "Player";
    // Use this for initialization
    void Start()
    {
        //_parent = this.gameObject.transform.parent.gameObject;
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = UpdateShadowPosition();
    }

    public Vector3 UpdateShadowPosition()
    {
        var playerPos = _player.GetComponent<TestPlayer>().pseudo3DPosition;
        shadowPseudo3DPos = GetProjectedLandingFromPseudo3DPosition(playerPos);
        return new Vector3(shadowPseudo3DPos.x, shadowPseudo3DPos.y + shadowPseudo3DPos.z, 0);
    }

}
