using UnityEngine;
using System.Collections;

public class AttackIconAnimScript : MonoBehaviour
{
    public float timeUntilDelete = 0.7f; //referenced in DestroyAfterTime.BattleActions.cs 
    public float maxScale, minScale, speed, minSpeed, timeFull;
    private Coroutine _coroutine;

    private void Start()
    {
        StartCoroutine(AttackIconAnimScriptTask(this.gameObject, maxScale, minScale, speed, minSpeed, timeFull));
    }

    private IEnumerator AttackIconAnimScriptTask(GameObject obj, float maxScale, float minScale, float speed, float minSpeed, float timeFull)
    {
        float timer = 0;
        float scale = 1;

        while (true) 
        {

            if (maxScale < minScale)
                maxScale = minScale;

            timer = 0;
            while(timer < timeUntilDelete)
            {
                timer += Time.deltaTime;
                
                float x = timer;
                float y = (maxScale) * (-8) * (x) * (x - timeUntilDelete); //parabola over time

                scale = y;
                obj.transform.localScale = new Vector3(scale, scale);
                yield return null;
            }
           
            timer = 0;

            Destroy(obj); //after animation is done, destroy the obj.
        }
    }

}
