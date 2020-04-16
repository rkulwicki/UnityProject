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
            damage = 1,
            attackIcon = Resources.Load("Prefabs/AttackIcons/Pow") as GameObject
        };
    }


    //MAKE 2 BADGES FOR THE ENEMY STATS FOR TESTING THE BUTTON ATTACK LIST
    public AttackBadge XAttackBadge()
    {
        return new AttackBadge()
        {
            badgeName = "X Attack",
            bpCost = 1,
            description = "X marks the spot. You can attack in the corners of the",
            isEquipped = true,
            isAcquired = true,
            range = new Vector3Int[4]{
                new Vector3Int(-1, 1, 0),  new Vector3Int(1, 1, 0 ),    
                new Vector3Int(-1, -1, 0), new Vector3Int(1, -1, 0 )
            },
            numberToHit = 1,
            damage = 2,
            attackIcon = Resources.Load("Prefabs/AttackIcons/Pow") as GameObject
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
            damage = 1,
            attackIcon = Resources.Load("Prefabs/AttackIcons/Pow") as GameObject
        };
    }


    //===== TEST =====
    public AttackBadge[] TestMake2Badges()
    {
        return new AttackBadge[2]
        {
            XAttackBadge(),
            PunchAttackBadge()
        };
    }

    #endregion

    #region Move Badges


    #endregion

    #region Stat Badges


    #endregion
}
