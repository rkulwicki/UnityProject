using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ActorStats
{
    public int playerLevel;

    public int maxFP;
    public int currentFP;

    public int currentBP;

    public int playerCoins;
    public int playerExperience;

    public AttackBadge[] attacks;

    public AttackBadge[] equippedBadges;

    private void Start()
    {
        attacks = new BadgeFactory().TestMake2Badges();
    }
}
