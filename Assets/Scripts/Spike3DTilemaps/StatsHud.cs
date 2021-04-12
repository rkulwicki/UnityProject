using UnityEngine;
using System.Collections;
using static Globals;

//Acts as a singleton 

public class StatsHud : MonoBehaviour
{
    public static StatsHud statsHud;

    public float distance;
    public bool canToggle;
    public bool isToggling;

    public bool isUp;
    private Vector2 panelPos;

    void Awake()
    {
        if (statsHud == null)
        {
            DontDestroyOnLoad(gameObject);
            statsHud = this;
        }
        else if (statsHud != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {

        panelPos = transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition;
        if (panelPos.y < 0.01 &&
           panelPos.y > -0.01)
        {
            isUp = true;
        }
        else
        {
            isUp = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canToggle)
        {
            if (Input.GetKeyDown(StatsKey))
            {
                if (!isToggling)
                {
                    SetIsToggling(true);
                    StartCoroutine(ToggleStatsHud(() => SetIsToggling(false)));
                }
            }
        }
    }

    private IEnumerator ToggleStatsHud(System.Action callback)
    {
        if (!isUp)
        {
            //do stuff
            var speed = 1;
            panelPos = transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition;
            var bottomBound = panelPos.y - distance;
            var initialPos = panelPos.y;

            float remainingDistance = distance;

            while (remainingDistance > 0)
            {
                Vector3 newPosition = Vector3.MoveTowards(panelPos, panelPos + new Vector2(0, -1), Time.deltaTime * speed);
                panelPos = newPosition;
                remainingDistance = panelPos.y - bottomBound;
                yield return null;
            }

        }
        else
        {
            //do stuff
            var speed = 1;
            panelPos = transform.GetChild(0).gameObject.GetComponent<RectTransform>().anchoredPosition;
            var topBound = 0;
            var initialPos = panelPos.y;

            float remainingDistance = distance;

            while (remainingDistance > 0)
            {
                Vector3 newPosition = Vector3.MoveTowards(panelPos, panelPos + new Vector2(0, -1), Time.deltaTime * speed);
                panelPos = newPosition;
                remainingDistance = panelPos.y - topBound;
                yield return null;
            }
        }

        yield return new WaitForSeconds(1f);
        callback();
    }

    private void SetIsToggling(bool isTog)
    {
        isToggling = isTog;
    }
}
