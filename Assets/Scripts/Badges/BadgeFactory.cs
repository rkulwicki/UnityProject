using UnityEngine;
using System.Collections;

public class BadgeFactory : MonoBehaviour
{
    #region Attack Badges
    public AttackBadge PunchAttackBadge()
    {
        return new AttackBadge()
        {
            badgeName = "Punch Attack",
            bpCost = 1,
            description = "Punching is the basic form of attack. The range is minimal and so is the damage but hey, it works!",
            isEquipped = true,
            isAcquired = true,
            range = new Vector3Int[8]{
                new Vector3Int(-1, 1, 0),  new Vector3Int(0, 1, 0 ),  new Vector3Int(1, 1, 0 ),
                new Vector3Int(-1, 0, 0),                             new Vector3Int(1, 0, 0),
                new Vector3Int(-1, -1, 0), new Vector3Int(0, -1, 0 ), new Vector3Int(1, -1, 0 )
            },
            numberToHit = 1,
            damage = 1
        };
    }


    //MAKE 2 BADGES FOR THE ENEMY STATS FOR TESTING THE BUTTON ATTACK LIST
    public AttackBadge ANOTHERAttackBadge()
    {
        return new AttackBadge()
        {
            badgeName = "Kick Face Attack",
            bpCost = 1,
            description = "Punching is the basic form of attack. The range is minimal and so is the damage but hey, it works!",
            isEquipped = true,
            isAcquired = true,
            range = new Vector3Int[8]{
                new Vector3Int(-1, 1, 0),  new Vector3Int(0, 2, 0 ),  new Vector3Int(1, 1, 0 ),
                new Vector3Int(-2, 0, 0),                             new Vector3Int(2, 0, 0),
                new Vector3Int(-1, -1, 0), new Vector3Int(0, -2, 0 ), new Vector3Int(1, -1, 0 )
            },
            numberToHit = 1,
            damage = 2
        };
    }

    //TEST
    public AttackBadge[] TestMake2Badges()
    {
        return new AttackBadge[2]
        {
            ANOTHERAttackBadge(),
            PunchAttackBadge()
        };
    }

    public AttackBadge PlusAttackBadge()
    {
        return new AttackBadge()
        {
            badgeName = "Plus Attack",
            bpCost = 1,
            description = "Plus Attack can hit up, down, left, and right.",
            isEquipped = true,
            isAcquired = true,
            range = new Vector3Int[4]{
                                          new Vector3Int(0, 1, 0 ), 
                new Vector3Int(-1, 0, 0),                             new Vector3Int(1, 0, 0),
                                          new Vector3Int(0, -1, 0 ),
            },
            numberToHit = 1,
            damage = 1
        };
    }
    #endregion

    #region Move Badges


    #endregion

    #region Stat Badges


    #endregion
}
