using UnityEngine;
using System.Collections;
using static Globals;

public class InteractiveObject : MonoBehaviour
{ 
    //Interaction Types
    public enum InteractionType
    {
        Shake,
        Nothing
    }

    public InteractionType interactionType;
    public float speed;
    public bool isShaking;
    public float distance;
    public int timesToShake;

    private BoxCollider2D _boxColliderTrigger;

    // Use this for initialization
    void Start()
    {
        _boxColliderTrigger = this.GetComponent<BoxCollider2D>();
        if (_boxColliderTrigger == null)
            Debug.Log(this.name + " does not have a BoxCollider2D attached. Please attach one.");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == PlayerTag && !isShaking)
        {
            StartCoroutine(Shake(this.gameObject));
        }
    }

    public IEnumerator Shake(GameObject go)
    {
        isShaking = true;
        var remainingDistance = distance;
        var leftBound = this.transform.position.x - distance;
        var rightBound = this.transform.position.x + distance;
        var initialPos = this.transform.position.x;
        for (int i = 0; i < timesToShake; i++)
        {
            remainingDistance = distance;
            while (remainingDistance > 0)
            {
                Vector3 newPosition = Vector3.MoveTowards(go.transform.position, go.transform.position + new Vector3(-1, 0, 0), Time.deltaTime * speed);
                transform.position = newPosition;
                remainingDistance = transform.position.x - leftBound;
                yield return null;
            }

            remainingDistance = distance;
            while (remainingDistance > 0)
            {
                Vector3 newPosition = Vector3.MoveTowards(go.transform.position, go.transform.position + new Vector3(1, 0, 0), Time.deltaTime * speed);
                transform.position = newPosition;
                remainingDistance = rightBound - transform.position.x;
                yield return null;
            }
        }
        //go back to initial position
        remainingDistance = distance;
        while (remainingDistance > 0)
        {
            Vector3 newPosition = Vector3.MoveTowards(go.transform.position, go.transform.position + new Vector3(-1, 0, 0), Time.deltaTime * speed);
            transform.position = newPosition;
            remainingDistance = transform.position.x - initialPos;
            yield return null;
        }
        transform.position = new Vector3(initialPos, transform.position.y);
        yield return new WaitForSeconds(1);
        isShaking = false;

    }
}
