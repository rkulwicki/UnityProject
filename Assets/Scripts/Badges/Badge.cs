using UnityEngine;
using System.Collections;

public enum BadgeType { ATTACK, STAT, MOVE, OTHER };
public class Badge : MonoBehaviour
{
    public string name;
    public BadgeType badgeType;
    public int bpCost;
    public string description;
    public bool isEquipped;
    public bool isAcquired;
}
