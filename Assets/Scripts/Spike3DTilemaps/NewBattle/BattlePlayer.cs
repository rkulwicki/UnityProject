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
    public bool grounded;

    private Rigidbody2D _body2d;
    private BoxCollider2D _box2d;
    private float _groundedDistance, _sideGroundedDistance;
    private float _convertedJumpPower, _convertedAcceleration, _convertedSpeed;
    private Vector2 _pos;
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
        _box2d = this.gameObject.GetComponent<BoxCollider2D>();
        _groundedDistance = _box2d.size.y / 2 + 0.1f;
        _sideGroundedDistance = _box2d.size.x / 2;
        _pos = this.gameObject.transform.position;

        ConfigurePlayerMovementStats();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayerBattle();
        CheckIfGrounded();
        JumpPlayerBattle();
    }

    private void ConfigurePlayerMovementStats()
    {
        //todo using speed, acceleration, jumpPower
        _convertedJumpPower = ConvertedJumpPower(jumpPower);
        _convertedAcceleration = ConvertedAcceleration(acceleration);
        _convertedSpeed = ConvertedSpeed(speed);
    }

    private void MovePlayerBattle()
    {
        if (Input.GetKey(XPositiveKey))
        {
            if (_body2d.velocity.x < speed)
            {
                _body2d.AddForce(new Vector2(acceleration, 0));
                //_pos = Vector2.MoveTowards(_pos, _pos + new Vector2(1, _pos.y), Time.deltaTime * speed);
            }
        }
        else if (Input.GetKey(XNegativeKey))
        {
            if (_body2d.velocity.x > -speed)
            {
                _body2d.AddForce(new Vector2(-acceleration, 0));
                //_pos = Vector2.MoveTowards(_pos, _pos + new Vector2(-1, _pos.y), Time.deltaTime * speed);
            }
        }

        //apply movement
        //transform.position = _pos;
    }

    private void JumpPlayerBattle()
    {
        if (Input.GetKey(JumpKey1) && grounded)
            _body2d.velocity = new Vector2(_body2d.velocity.x, jumpPower);
    }

    private void CheckIfGrounded()
    {
        var hitCenter = Physics2D.Raycast(transform.position, Vector2.down, _groundedDistance);
        var hitRight = Physics2D.Raycast(transform.position + new Vector3(_sideGroundedDistance, 0,0), Vector2.down, _groundedDistance);
        var hitLeft = Physics2D.Raycast(transform.position - new Vector3(_sideGroundedDistance, 0, 0), Vector2.down, _groundedDistance);

        Debug.DrawLine(transform.position, transform.position - new Vector3(0, _groundedDistance, 0), Color.red);
        Debug.DrawLine(transform.position + new Vector3(_sideGroundedDistance, 0, 0), transform.position - new Vector3(-_sideGroundedDistance, _groundedDistance, 0), Color.red);
        Debug.DrawLine(transform.position - new Vector3(_sideGroundedDistance, 0, 0), transform.position - new Vector3(_sideGroundedDistance, _groundedDistance, 0), Color.red);

        if (hitCenter || hitRight || hitLeft)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    private float ConvertedJumpPower(int jumpPower)
    {
        switch (jumpPower)
        {
            case 1:
                return 5;
            case 2:
                return 5.5f;
            case 3:
                return 6f;
            case 4:
                return 6.5f;
            case 5:
                return 7f;
            case 6:
                return 7.5f;
            case 7:
                return 8f;
            case 8:
                return 8.5f;
            case 9:
                return 9f;
            case 10:
                return 9.5f;
            default:
                return 5;
        }
    }

    private float ConvertedSpeed(int speed)
    {
        switch (speed)
        {
            case 1:
                return 1;
            case 2:
                return 2f;
            case 3:
                return 3f;
            case 4:
                return 4f;
            case 5:
                return 5f;
            case 6:
                return 6f;
            case 7:
                return 7f;
            case 8:
                return 8f;
            case 9:
                return 9f;
            case 10:
                return 10;
            default:
                return 1;
        }
    }

    private float ConvertedAcceleration(int acceleration)
    {
        switch (acceleration)
        {
            case 1:
                return 1;
            case 2:
                return 2f;
            case 3:
                return 3f;
            case 4:
                return 4f;
            case 5:
                return 5f;
            case 6:
                return 6f;
            case 7:
                return 7f;
            case 8:
                return 8f;
            case 9:
                return 9f;
            case 10:
                return 10;
            default:
                return 1;
        }
    }
}
