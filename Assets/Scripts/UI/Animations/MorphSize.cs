using UnityEngine;
using System.Collections;

public class MorphSize : MonoBehaviour
{
    public float maxScale, minScale, speed;
    private Coroutine _coroutine;

    private void Start()
    {
        StartCoroutine(MorphSizeTask(this.gameObject, maxScale, minScale, speed));
    }

    public bool StartMorph(float maxScale, float minScale, float speed, GameObject obj)
    {
        if (_coroutine == null)
        {
            _coroutine = StartCoroutine(MorphSizeTask(obj, maxScale, minScale, speed));
            return true;
        }
        return false;
    }

    public bool StopMorph()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            return true;
        }
        return false;
    }

    private IEnumerator MorphSizeTask(GameObject obj, float maxScale, float minScale, float speed)
    {
        float timer = 0;
        float scale = 1;

        while (true) 
        {

            if (maxScale < minScale)
                maxScale = minScale;

            while (maxScale > scale)
            {
                timer += Time.deltaTime;
                scale += Time.deltaTime * speed;
                obj.transform.localScale = new Vector3(scale, scale);
                yield return null;
            }

            timer = 0; // reset the timer
            while (minScale < scale)
            {
                timer += Time.deltaTime;
                scale -= Time.deltaTime * speed;
                obj.transform.localScale = new Vector3(scale, scale);
                yield return null;
            }

            timer = 0;
        }
    }

}
