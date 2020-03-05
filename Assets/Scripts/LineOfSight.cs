using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    private EnemyStats _enemyStats;
    private Transform _trans;
    private SpriteRenderer _spriteRend;
    private BoxCollider2D _col2D;
    void Start()
    {
        _enemyStats = gameObject.GetComponentInParent<EnemyStats>();
        _trans = gameObject.transform;
        int scale = _enemyStats.enemySightRadius * 2;
        _trans.localScale = new Vector3(scale, scale, 1);
        _spriteRend = gameObject.GetComponent<SpriteRenderer>();
    }
    
}
