using UnityEngine;
using System.Collections;
using static Globals;
using static InteractiveObjectTypeRepository;
public class InteractiveObject : MonoBehaviour
{ 
    public InteractionType interactionType;
    public bool isActive;

    private BoxCollider2D _boxColliderTrigger;
    private GameObject _player;
    void Start()
    {
        _boxColliderTrigger = this.GetComponent<BoxCollider2D>();
        if (_boxColliderTrigger == null)
            Debug.Log(this.name + " does not have a BoxCollider2D attached. Please attach one.");
        _player = GameObject.FindGameObjectWithTag(PlayerTag);
    }

    private void Update()
    {
        if (_player.GetComponent<PlayerInteractionWithObjects>().InteractiveObject == this.gameObject &&
            !isActive)
        {
            // On button press, start interaction
            if (Input.GetKeyDown(InteractKey))
            {
                switch (interactionType)
                {
                    case InteractionType.Shake:
                        SetActive(true);
                        StartCoroutine(Shake(this.gameObject, () => SetActiveAndPlayerInteractionObject(false)));
                        break;

                    case InteractionType.Nothing:
                        SetActive(true);
                        StartCoroutine(Nothing(this.gameObject, () => SetActiveAndPlayerInteractionObject(false)));
                        break;

                    default:
                        break;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == PlayerTag && !isActive)
        {
            // You are within range of interacting
            SetPlayerInteractionObject(true);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        //if you leave before interacting, the object should be null
        var piwo = _player.GetComponent<PlayerInteractionWithObjects>();
        if (col.gameObject.tag == PlayerTag && !isActive && piwo.InteractiveObject == this.gameObject)
            piwo.InteractiveObject = null;
    }

    public void SetActiveAndPlayerInteractionObject(bool val)
    {
        SetActive(val);
        SetPlayerInteractionObject(val);
    }

    private void SetActive(bool val)
    {
        isActive = val;
        _player.GetComponent<PlayerInteractionWithObjects>().isActive = val;
    }

    private void SetPlayerInteractionObject(bool val)
    {
        var piwo = _player.GetComponent<PlayerInteractionWithObjects>();

        if (val)
            piwo.InteractiveObject = this.gameObject;
        else
            piwo.InteractiveObject = null;
    }
}
