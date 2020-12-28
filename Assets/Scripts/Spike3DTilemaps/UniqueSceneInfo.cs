using UnityEngine;
using System.Collections;
using static Globals;
using System;
using UnityEngine.UI;
using static SceneChangingFunctions;
using static PlayerStaticFunctions;
public class UniqueSceneInfo : MonoBehaviour
{

    public Vector3[] previousScenesLocations;
    public SceneNames[] previousScenes;
    public bool isScreenFading;// { get; private set; }
    
    // On awake, whatever the previous scene was determines where the player will load into the scene.
    void Start()
    {
        AwakeScene();
    }

    private void AwakeScene()
    {
        //instantiate player
        var player = GetOrInstantiatePlayer(new Vector3(0,0,0), new Quaternion());

        //player position
        for (int i = 0; i < previousScenes.Length; i++)
        {
            var t = PersistentData.data.previousScene;
            if (PersistentData.data.previousScene == previousScenes[i])
            {
                GameObject.FindGameObjectWithTag(PlayerTag).GetComponent<Pseudo3DPlayer>().pseudo3DPosition = previousScenesLocations[i];
                break;
            }
        }

        //On load, screen fades up. TODO - lock player
        SetIsScreenFading(true);
        StartCoroutine(ScreenFadeUp(() => SetIsScreenFading(false), FadeToBlackTime, BeforeFadeToBlackTime));
        //StartCoroutine(SuspendPlayerMovementForGivenTime(() => { }, (FadeToBlackTime + BeforeFadeToBlackTime)));
    }

    public void SetIsScreenFading(bool val)
    {
        isScreenFading = val;
    }

}
