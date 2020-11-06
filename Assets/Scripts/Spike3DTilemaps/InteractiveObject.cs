using UnityEngine;
using System.Collections;
using static Globals;
using static InteractiveObjectTypeRepository;
public class InteractiveObject : MonoBehaviour
{ 
    public InteractionType interactionType;
    public bool isActive;

    private BoxCollider2D _boxColliderTrigger;

    void Start()
    {
        _boxColliderTrigger = this.GetComponent<BoxCollider2D>();
        if (_boxColliderTrigger == null)
            Debug.Log(this.name + " does not have a BoxCollider2D attached. Please attach one.");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == PlayerTag && !isActive)
        {
            switch (interactionType)
            {
                case InteractionType.Shake:
                    SetActive(true);
                    StartCoroutine(Shake(this.gameObject, () => SetActive(false)));
                    break;

                case InteractionType.Nothing:
                    SetActive(true);
                    StartCoroutine(Nothing(this.gameObject, () => SetActive(false)));
                    break;

                default:
                    break;
            }
           
        }
    }

    public void SetActive(bool val)
    {
        isActive = val;
    }
}
