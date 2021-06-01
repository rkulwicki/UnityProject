using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static Globals;

namespace Assets.Scripts.Spike3DTilemaps.UI
{
    public class ExperienceUIText : MonoBehaviour
    {

        public string displayText;

        private PersistentData persistentData;
        private Text text;
        // Use this for initialization
        void Start()
        {
            persistentData = GameObject.FindGameObjectWithTag(PersistentDataTag).GetComponent<PersistentData>();
            text = this.gameObject.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = persistentData.experience.ToString();
        }
    }
}