using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeTrigger : MonoBehaviour
{
    public string nextScene;
    public Vector3 posInNextScene;
    public Vector3 endPositionInCurrentScene;

    private SceneChanger _sceneChanger;
    // Start is called before the first frame update
    void Start()
    {
        _sceneChanger = GameObject.FindGameObjectWithTag("SceneChanger").GetComponent<SceneChanger>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            _sceneChanger.PlayerMoveToNewScene(endPositionInCurrentScene, nextScene, posInNextScene);
        }
    }
}
