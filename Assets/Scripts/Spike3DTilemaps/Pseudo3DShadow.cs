using UnityEngine;
using System.Collections;
using static Pseudo3DTilemapLogic;
using static Globals;

public class Pseudo3DShadow : MonoBehaviour
{
    public float groundedScale;
    public Vector3 shadowPseudo3DPos;

    private float _distanceFromGround;
    private GameObject _player;

    void Start()
    {
        //_parent = this.gameObject.transform.parent.gameObject;
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
    }

    void Update()
    {
        this.transform.position = UpdateShadowPosition();
        UpdateSpriteRenderer();
    }

    public Vector3 UpdateShadowPosition()
    {
        var playerPos = _player.GetComponent<Pseudo3DPosition>().pseudo3DPosition;
        shadowPseudo3DPos = GetProjectedLandingFromPseudo3DPosition(playerPos);
        _distanceFromGround = _player.GetComponent<Pseudo3DPosition>().distanceFromGround;
        return new Vector3(shadowPseudo3DPos.x, shadowPseudo3DPos.y + shadowPseudo3DPos.z, 0);
    }

    //todo make shadow get bigger and fuzzier the further distance you are from the bottom
    private void UpdateSpriteRenderer()
    {
        //distanceFromGround
        var scale = (1)/(1 + _distanceFromGround) * groundedScale; //after every 1 unit up, we cut the size in half
        this.transform.localScale = new Vector3(scale, scale, 1);
    }
}
