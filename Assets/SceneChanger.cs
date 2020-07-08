using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public Direction directionToMove;

    private GameObject playerToMove;

    private float speedToMove;
    private float timeToFadeToBlack;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }



    TODO


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player") {
            playerToMove = col.gameObject;
            StartCoroutine(MoveOverSeconds(playerToMove, speedToMove, directionToMove));
        }
    }


    private IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }
}
