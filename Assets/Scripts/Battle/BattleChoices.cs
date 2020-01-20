using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleChoicesEnum { NONE, ATTACK, MOVE }

public class BattleChoices : BattleActions
{
    public BattleChoicesEnum battleChoice;
}
