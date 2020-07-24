using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public float timeToMove;
    public bool changing;

    private GameObject _mainCamera;
    private bool _fadeToBlack, _fadeFromBlack;
    private GameObject _player;
    private string _nextScene;
    private Vector3 _posInNextScene;
    private Vector3 _endPosition;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void PlayerMoveToNewScene(Vector3 endPositionInCurrentScene, string nextScene, Vector3 posInNextScene)
    {
        _nextScene = nextScene;
        _endPosition = endPositionInCurrentScene;
        MoveToPosition(_player.transform, _endPosition, timeToMove);
        FadeToBlack();
        WaitForFade(_player);
    }

    //TODO - Move this to a new script to be reused. Something like CutSceneFunctions or something.
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
            transform.position = Vector3.Lerp(currentPos, position, t); //transform has been destroyed but you're still trying to access it
            yield return null;
        }
    }

    public void MoveObjectToNewScene(GameObject gameObj, Vector3 posInScene, string sceneName)
    {
        var scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.MoveGameObjectToScene(gameObj, scene);

    }

    public void FadeToBlack()
    {
        StartCoroutine(FadeToBlackCoroutine(timeToMove));
    }

    private IEnumerator FadeToBlackCoroutine(float time)
    {
        _fadeToBlack = true;

        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        var blackCanvasGroup = _mainCamera.transform.Find("BlackCanvas").GetComponent<CanvasGroup>();

        while (blackCanvasGroup.alpha < 1)
        {
            blackCanvasGroup.alpha += Time.deltaTime / time;
            yield return null;
        }

        _fadeToBlack = false;
        yield return null;
    }

    public void FadeFromBlack()
    {
        StartCoroutine(FadeFromBlackCoroutine(timeToMove));
    }

    private IEnumerator FadeFromBlackCoroutine(float time)
    {
        _fadeFromBlack = true;

        var mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        var blackCanvasGroup = _mainCamera.transform.Find("BlackCanvas").GetComponent<CanvasGroup>();

        while (blackCanvasGroup.alpha > 0)
        {
            blackCanvasGroup.alpha -= Time.deltaTime / time;
            yield return null;
        }

        _fadeFromBlack = false;
        yield return null;
    }

    public void WaitForFade(GameObject gameObj)
    {
        StartCoroutine(WaitForFadeCoroutine(gameObj));
    }

    private IEnumerator WaitForFadeCoroutine(GameObject gameObj)
    {
        while (_fadeToBlack)
        {
            yield return null;
        }
 
        SceneManager.LoadScene(_nextScene);
        //MoveObjectToNewScene(gameObj, _posInNextScene, _nextScene);
        //FadeFromBlack();
    }

}
