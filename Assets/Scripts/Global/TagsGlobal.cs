using UnityEngine;
using System.Collections;

public class TagsGlobal : MonoBehaviour
{
    public string[] tagsToIgnoreInBattle;
    public TagsGlobal()
    {
        tagsToIgnoreInBattle = new string[1] { "NPC"};
    }
}
