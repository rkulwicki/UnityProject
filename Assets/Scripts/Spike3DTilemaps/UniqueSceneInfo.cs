using UnityEngine;
using System.Collections;
using static Globals;

public class UniqueSceneInfo : MonoBehaviour
{

    public Vector3[] previousScenesLocations;
    public SceneNames[] previousScenes;
    

    // On awake, whatever the previous scene was determines where the player will load into the scene.
    void Start()
    {
        AwakeScene();
    }

    private void AwakeScene()
    {
        for(int i = 0; i < previousScenes.Length; i++)
        {
            var t = PersistentData.data.previousScene;
            if (PersistentData.data.previousScene == previousScenes[i])
            {
                GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Pseudo3DPlayer>().pseudo3DPosition = previousScenesLocations[i];
                break;
            }
        }
    }
}
