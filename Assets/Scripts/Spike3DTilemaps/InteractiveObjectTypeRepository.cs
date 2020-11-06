using UnityEngine;
using System.Collections;
using System;

public static class InteractiveObjectTypeRepository
{

    //Interaction Types
    public enum InteractionType
    {
        Shake,
        Nothing
    }

    /// <summary>
    /// Shake the object back and forth just a little bit before returning to OG position.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static IEnumerator Shake(GameObject go, Action callback)
    {
        // ---
        //can move these out if I want to make this more variable?
        var speed = 1;
        var distance = 0.05f;
        var timesToShake = 3;
        // ---

        var leftBound = go.transform.position.x - distance;
        var rightBound = go.transform.position.x + distance;
        var initialPos = go.transform.position.x;

        float remainingDistance;
        for (int i = 0; i < timesToShake; i++)
        {
            remainingDistance = distance;
            while (remainingDistance > 0)
            {
                Vector3 newPosition = Vector3.MoveTowards(go.transform.position, go.transform.position + new Vector3(-1, 0, 0), Time.deltaTime * speed);
                go.transform.position = newPosition;
                remainingDistance = go.transform.position.x - leftBound;
                yield return null;
            }

            remainingDistance = distance;
            while (remainingDistance > 0)
            {
                Vector3 newPosition = Vector3.MoveTowards(go.transform.position, go.transform.position + new Vector3(1, 0, 0), Time.deltaTime * speed);
                go.transform.position = newPosition;
                remainingDistance = rightBound - go.transform.position.x;
                yield return null;
            }
        }
        //go back to initial position
        remainingDistance = distance;
        while (remainingDistance > 0)
        {
            Vector3 newPosition = Vector3.MoveTowards(go.transform.position, go.transform.position + new Vector3(-1, 0, 0), Time.deltaTime * speed);
            go.transform.position = newPosition;
            remainingDistance = go.transform.position.x - initialPos;
            yield return null;
        }
        go.transform.position = new Vector3(initialPos, go.transform.position.y);
        yield return new WaitForSeconds(1);

        callback();
    }

    /// <summary>
    /// Does nothing. Probably will be used as default.
    /// </summary>
    /// <param name="go"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static IEnumerator Nothing(GameObject go, Action callback)
    {
        yield return new WaitForSeconds(1);
        callback();
    }
}
