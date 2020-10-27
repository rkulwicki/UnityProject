using UnityEngine;
using System;
using static Globals;
using UnityEngine.SceneManagement;

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

            ChangeThisScene(this.sceneName);
        }
    }

    public void ChangeThisScene(SceneNames sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }
}
