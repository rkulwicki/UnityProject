using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActions : MonoBehaviour
{

    private float timeUntilDestroyIcon;

    public ActorStats Heal(int toHealBy, ActorStats targetStats)
    {
        targetStats.currentHP = targetStats.currentHP + toHealBy;
        if (targetStats.currentHP > targetStats.maxHP)
        {
            targetStats.currentHP = targetStats.maxHP;
        }

        return targetStats;
    }

    public ActorStats Attack(int toAttackBy, ActorStats targetStats)
    {
        int attackByWithDefense = toAttackBy - targetStats.baseDefense;
        if (attackByWithDefense < 0)
        {
            attackByWithDefense = 0;
        }
        targetStats.currentHP = targetStats.currentHP - attackByWithDefense;
        if (targetStats.currentHP < 0)
        {
            targetStats.currentHP = 0;
        }

        return targetStats;
    }

    public ActorStats Attack(int toAttackBy, ActorStats targetStats, GameObject attackIcon, Vector3 posToSpawn)
    {
        var attackIconComp = gameObject.AddComponent<AttackIcon>();
        attackIconComp.attackIconPrefab = attackIcon;
        var tempIcon = attackIconComp.Spawn(posToSpawn, toAttackBy);

        return Attack(toAttackBy, targetStats);
    }

    public ActorStats Move(int toMoveBy, ActorStats targetStats)
    {
        //TODO
        return targetStats;
    }
} 
