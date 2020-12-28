using UnityEngine;
using System.Collections;
using static Globals;
using System;
using UnityEngine.UI;
using static PlayerStaticFunctions;
public static class SceneChangingFunctions
{
    /// <summary>
    /// Fades a black overlay from alpha 1 to 0 for a transition-into effect
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="timeToFade"></param>
    /// <returns></returns>
    public static IEnumerator ScreenFadeDown(Action callback, float timeToFade, float timeBeforeFade)
    {
        var blackScreen = Resources.Load(BlackScreenPath) as GameObject;
        var tempBlackScreen = UnityEngine.Object.Instantiate(blackScreen);
        var c = tempBlackScreen.transform.GetChild(0).GetChild(0).GetComponent<Image>().color;
        tempBlackScreen.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0); //starts alpha = 0
        var timer = 0f;

        yield return new WaitForSeconds(timeBeforeFade);

        while (timer < timeToFade)
        {
            timer += Time.deltaTime;
            tempBlackScreen.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(c.r, c.g, c.b, timer / timeToFade);
            yield return null;
        }

        callback();
    }

    /// <summary>
    /// Fades a black overlay from alpha 0 to 1 for a transition-out effect
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="timeToFade"></param>
    /// <returns></returns>
    public static IEnumerator ScreenFadeUp(Action callback, float timeToFade, float timeBeforeFade)
    {
        var blackScreen = Resources.Load(BlackScreenPath) as GameObject;
        var tempBlackScreen = UnityEngine.Object.Instantiate(blackScreen);
        var c = tempBlackScreen.transform.GetChild(0).GetChild(0).GetComponent<Image>().color;
        tempBlackScreen.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1); //starts alpha = 1
        var timer = 0f;

        yield return new WaitForSeconds(timeBeforeFade);

        while (timer < timeToFade)
        {
            timer += Time.deltaTime;
            tempBlackScreen.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1 - timer / timeToFade);
            yield return null;
        }

        UnityEngine.Object.Destroy(tempBlackScreen);
        callback();
    }

    public static IEnumerator SuspendPlayerMovementForGivenTime(Action callback, float time)
    {
        SuspendPlayerMovement(true);
        yield return new WaitForSeconds(time);
        SuspendPlayerMovement(false);
        callback();
    }
}
