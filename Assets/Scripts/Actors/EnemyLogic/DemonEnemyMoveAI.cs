﻿using UnityEngine;
using System.Collections;
using System;

public class DemonEnemyMoveAI : EnemyMoveAI
{
    public float randLow = 0.5f;
    public float randUp = 3f;

    private bool waiting = false;

    private Coroutine co;

    // Update is called once per frame
    void Update()
    {
        if (battleManager.state == BattleState.INACTIVE && !waiting) //move if not battle or not waiting
        {
            var rnd = new System.Random();
            var rand = (rnd.NextDouble() * (randUp - randLow)) + randLow;
            co = StartCoroutine(Wait(Convert.ToSingle(rand)));

        }
    }

    public IEnumerator Wait(float time)
    {
        waiting = true;
        yield return new WaitForSeconds(time);
        MoveRandomDirectionAvoidObstacles();
        waiting = false;
    }
}
