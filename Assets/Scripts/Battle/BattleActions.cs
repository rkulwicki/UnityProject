using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActions : MonoBehaviour
{

    public ActorStats Heal(int toHealBy, ActorStats actorStats)
    {
        actorStats.currentHP = actorStats.currentHP + toHealBy;
        if (actorStats.currentHP > actorStats.maxHP)
        {
            actorStats.currentHP = actorStats.maxHP;
        }

        return actorStats;
    }

    public ActorStats Attack(int toAttackBy, ActorStats actorStats)
    {
        int attackByWithDefense = toAttackBy - actorStats.baseDefense;
        if (attackByWithDefense < 0)
        {
            attackByWithDefense = 0;
        }
        actorStats.currentHP = actorStats.currentHP - attackByWithDefense;
        if (actorStats.currentHP < 0)
        {
            actorStats.currentHP = 0;
        }

        return actorStats;
    }
}
