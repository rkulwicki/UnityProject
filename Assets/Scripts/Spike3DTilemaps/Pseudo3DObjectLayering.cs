using UnityEngine;
using System.Collections;
using static Globals;

public class Pseudo3DObjectLayering : MonoBehaviour
{
    public Vector3 pseudo3DPosition;

    private Vector3 _playerPseudo3DPosition;
    // Use this for initialization
    void Start()
    {
        _playerPseudo3DPosition = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Pseudo3DPosition>().pseudo3DPosition;
    }

    // Update is called once per frame
    void Update()
    {
        _playerPseudo3DPosition = GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Pseudo3DPosition>().pseudo3DPosition;
        ChangeLayerByPlayerPseudo3DPosition();
    }

    private void ChangeLayerByPlayerPseudo3DPosition()
    {   // below -> above
        if (this.GetComponent<SpriteRenderer>().sortingLayerName == ObjectBelowTag &&
            _playerPseudo3DPosition.y > pseudo3DPosition.y ||
            _playerPseudo3DPosition.z < pseudo3DPosition.z)
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = ObjectAboveTag;
        }
        // above -> below
        else if (this.GetComponent<SpriteRenderer>().sortingLayerName == ObjectAboveTag &&
            _playerPseudo3DPosition.y <= pseudo3DPosition.y)
        {
            this.GetComponent<SpriteRenderer>().sortingLayerName = ObjectBelowTag;
        }
    }
}
