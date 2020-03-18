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
            numberToHit = 1
        };
    }

    #endregion

    #region Move Badges


    #endregion

    #region Stat Badges


    #endregion
}
