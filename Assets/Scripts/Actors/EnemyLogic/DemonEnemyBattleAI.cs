using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class DemonEnemyBattleAI : EnemyBattleAI
{
    public bool beginLogic = false; // <--- should probably be in EnemyLogic but whatever
    public bool beginTurn = false;        // <---

    private int logicDictator = 1;

    private int steps = 3;

    //private int stepsCounter = 0;
    private void Start()
    {
        EnemyBattleAIStart();
        //setting attacks
        var list = new List<AttackBadge>();

        var badgeFactory = new BadgeFactory();

        list.Add(badgeFactory.PlusAttackBadge());

        enemyStats.attacks = list.ToArray();
        //====
    }

    private void Update()
    {
        CheckDead();

        if (!beginLogic || !beginTurn)
            return;

        //it's the enemy's turn. What does it do?
        if (logicDictator == 1) //Move then attack if in Plus Range
        {
            //move
            beginTurn = MoveTowardsActor(this.gameObject, player, steps);

            //Attack if in range
            if (!beginTurn) //do after move.
            {
                var attack = enemyStats.attacks[0]; //the attack :]
                var repositionedRange = Reposition(enemyStats.attacks[0].range, ConvertV3ToV3Int(this.transform.position));
                var maybePlayer = GetPlayerInRange(repositionedRange);
                if (maybePlayer != null)
                {
                    enemyActions.Attack(attack.damage, maybePlayer.GetComponent<PlayerStats>(), attack.attackIcon, 
                        maybePlayer.transform.position + new Vector3(0,0.5f,0)); //attack
                }
            }
        }
        else if (logicDictator == 2)
        {

            //TODO!!!!!
            beginTurn = false;
        }
            
    }
}
