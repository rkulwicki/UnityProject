using UnityEngine;
using System.Collections;
using System;

public class DemonEnemyLogic : EnemyLogic
{
    public bool beginLogic = false; // <--- should probably be in EnemyLogic but whatever
    public bool beginTurn = false;        // <---

    private int logicDictator = 0;
    private void Update()
    {
        if (!beginLogic || !beginTurn)
            return;
        //it's the enemy's turn. What does it do?
        if(logicDictator == 0) //MOVE
        {
            int rand = new System.Random().Next(1,4);
            switch (rand)
            {
                case 1:
                    MoveOneTile(Direction.UP);
                    break;
                case 2:
                    MoveOneTile(Direction.RIGHT);
                    break;
                case 3:
                    MoveOneTile(Direction.DOWN);
                    break;
                case 4:
                    MoveOneTile(Direction.LEFT);
                    break;
            }
            //logicDictator == 1
            beginTurn = false;
        }
        else if (logicDictator == 1){

        }
            
    }
}
