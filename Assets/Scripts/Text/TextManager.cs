using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{

    //will be used later VVVVV
    //public string textToOutput;

    public enum TextBoxTypeEnum
    {
        clickOut,
        walkOut,
        timed
    }
    public float textTime;

    public GameObject textBoxObject;

    public TextBoxTypeEnum textBoxTypeEnum;

    public string text;

    public GameObject textBoxPrefab; //using the textbox prefab
    public int waitSeconds;

    private GameObject playerObject;

    private bool inTextBoxClickOut = false;
    private bool inTextBox = false;

    private void Start()
    {
        playerObject = GameObject.Find("Player");
    }

    private void Update()
    {
        
        //inside a text box
        if (inTextBoxClickOut)
        {

            if (Input.anyKey)
            {
                //if last dialogue string:
                DestroyTextBox();
                playerObject.GetComponent<PlayerMove>().canMove = true;
                inTextBoxClickOut = false;
            }

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {

            if (textBoxTypeEnum == TextBoxTypeEnum.clickOut){
                TextBoxClickOut(); //TODO multiple boxes
            }


            else if(textBoxTypeEnum == TextBoxTypeEnum.walkOut){
                TextBoxWalkOut();
            }



            else if (textBoxTypeEnum == TextBoxTypeEnum.timed){
                TextBoxTimed(); //TODO activate timed
            }

        }
    }

    //TODO: bug in click out text box. Does not reappear.
    private void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            inTextBox = false;
        }
    }

    #region TextBoxTypesLogic - TextBoxyClickOut, TextBoxWalkOut, TextBoxTimed
    private void TextBoxClickOut()
    {
        //disable player movement
        inTextBoxClickOut = true;
        playerObject.GetComponent<PlayerMove>().canMove = false;
        SpawnTextBox(textBoxPrefab);


        //TODO
        //while there is still text to display,
        //  keep displayin
        //destroying happens in Update when clicked. So don't worry about it, bro!
    }

    private void TextBoxWalkOut()
    {
        SpawnTextBox(textBoxPrefab);
        //destroying happens in a function below (OnTriggerExit2D). So don't worry about it, bro!
    }

    private void TextBoxTimed()
    {
        playerObject.GetComponent<PlayerMove>().canMove = false;
        SpawnTextBox(textBoxPrefab);
        StartCoroutine(TimedWait());
    }
    #endregion

    #region HelperFunctions - OnTriggerExit2D, TypeText, TimedWait()

    private void OnTriggerExit2D(Collider2D col) //Only used for WalkOut
    {
        if(col.tag == "Player" && textBoxTypeEnum == TextBoxTypeEnum.walkOut)
        {
            DestroyTextBox();
        }
    }

    public void TypeText(string text, float timeToType)
    {
        //textToType = text;
        //var numOfChars = textToType.Length;
        //singleKeyDuration = timeToType / numOfChars;
        //textDisplayed = "";
        //timeStarted = Time.time;
    }

    IEnumerator TimedWait()
    {
        yield return new WaitForSeconds(waitSeconds);
        DestroyTextBox();
        playerObject.GetComponent<PlayerMove>().canMove = true;
    }

    #endregion

    #region CreateAndDestroy - SpawnTextBox, DestroyTextBox

    public void SpawnTextBox(GameObject textBoxPrefab)
    {
        textBoxObject = Instantiate(textBoxPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        textBoxObject.GetComponent<TextHolder>().text = text;
        //insert "text" into text box dialogue
    }

    public void DestroyTextBox()
    {
        Destroy(textBoxObject);
    }

    #endregion
}
