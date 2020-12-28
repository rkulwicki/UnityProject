using UnityEngine;
using System;
using static Globals;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using static SceneChangingFunctions;
public class SceneTrigger : MonoBehaviour
{
    public SceneNames sceneName;

    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.gameObject.tag == PlayerTag)
        {
            var result = SceneNames.Default;
            Enum.TryParse(SceneManager.GetActiveScene().name, out result);
            PersistentData.data.previousScene = result;

            //fade to black, then change scenes. TODO - lock player
            StartCoroutine(ScreenFadeDown(() => ChangeThisScene(this.sceneName), FadeToBlackTime, 0f));
            StartCoroutine(SuspendPlayerMovementForGivenTime(() => { }, FadeToBlackTime));
        }
    }

    public void ChangeThisScene(SceneNames sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
