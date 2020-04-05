using UnityEngine;
using System.Collections;
using System;

public class DemonEnemyBattleAI : EnemyBattleAI
{
    public bool beginLogic = false; // <--- should probably be in EnemyLogic but whatever
    public bool beginTurn = false;        // <---

    private int logicDictator = 1;

    private int stepsToMove = 3;

    private int stepsCounter = 0;

    private void Update()
    {
        CheckDead();

        if (!beginLogic || !beginTurn)
            return;

        //it's the enemy's turn. What does it do?
        if (logicDictator == 1) //MOVE
        {
            if (isMoving || inActionCooldown || _onExit || !canMove) return; //moving. don't try to move again

            MoveTowardsActor(this.gameObject, player);
            stepsCounter++;
            if (stepsCounter >= stepsToMove)
            {
                beginTurn = false; //turn ender.
                stepsCounter = 0;
            }
        }
        else if (logicDictator == 2)
        {

            //TODO!!!!!
            beginTurn = false;
        }
            
    }
}
