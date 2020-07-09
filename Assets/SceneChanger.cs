using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public Vector3 endPosition;
    public float timeToMove;

    private GameObject playerToMove;
    private GameObject mainCamera;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            playerToMove = col.gameObject;
            MoveToPosition(playerToMove.transform, endPosition, timeToMove);
            FadeToBlack();
        }
    }

    public void MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        StartCoroutine(MoveToPositionCoroutine(transform, position, timeToMove));
    }

    private IEnumerator MoveToPositionCoroutine(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }


    public void FadeToBlack()
    {
        StartCoroutine(FadeToBlackCoroutine(timeToMove));
    }

    private IEnumerator FadeToBlackCoroutine(float time)
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        var blackCanvasGroup = mainCamera.transform.Find("BlackCanvas").GetComponent<CanvasGroup>();

        while (blackCanvasGroup.alpha < 1)
        {
            blackCanvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }

        yield return null;
    }
}
