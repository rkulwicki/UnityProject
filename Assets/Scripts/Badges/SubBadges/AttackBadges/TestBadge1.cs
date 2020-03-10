using UnityEngine;
using System.Collections;

public class TestBadge1 : AttackBadge
{
    public TestBadge1()
    {
        this.badgeType = BadgeType.ATTACK;
        this.bpCost = 1;
        this.description = "This is a test badge and I hope I don't find any bugs!";
        this.name = "Test Badge";
        this.range = new Vector3Int[4] {
            new Vector3Int(1, 0, 0),
            new Vector3Int(-1,0,0),
            new Vector3Int(0,1,0),
            new Vector3Int(0,-1,0)
        };
    }
}
