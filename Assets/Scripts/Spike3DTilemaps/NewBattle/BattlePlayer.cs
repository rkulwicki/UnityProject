using UnityEngine;
using System.Collections;
using static Globals;
public class BattlePlayer : MonoBehaviour
{

    public int level;
    public int experience;
    public int health;
    public int attackPower;
    public int defensePower;
    public int speed;
    public int acceleration;
    public int jumpPower;

    private Rigidbody2D _body2d;
    // Use this for initialization
    void Start()
    {
        level = PersistentData.data.level;
        experience = PersistentData.data.experience;
        health = PersistentData.data.health;
        attackPower = PersistentData.data.attackPower;
        defensePower = PersistentData.data.defensePower;
        speed = PersistentData.data.speed;
        acceleration = PersistentData.data.acceleration;
        jumpPower = PersistentData.data.jumpPower;

        _body2d = this.gameObject.GetComponent<Rigidbody2D>();
        ConfigurePlayerMovementStats();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayerBattle();
        JumpPlayerBattle();

    }

    private void ConfigurePlayerMovementStats()
    {
        //todo using speed, acceleration, jumpPower
    }

    private void MovePlayerBattle()
    {

        if (Input.GetKey(XPositiveKey))
            _body2d.AddForce(new Vector2(speed,0));
        else if (Input.GetKey(XNegativeKey))
            _body2d.AddForce(new Vector2(-speed, 0));
    }

    private void JumpPlayerBattle()
    {
        if (Input.GetKey(JumpKey1))
            _body2d.velocity = new Vector2(_body2d.velocity.x, jumpPower);
    }
}
