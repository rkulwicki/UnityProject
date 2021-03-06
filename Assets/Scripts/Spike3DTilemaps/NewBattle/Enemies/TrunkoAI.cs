using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Spike3DTilemaps.NewBattle.Enemies
{
    public class TrunkoAI : MonoBehaviour
    {
        public float speed;
        public bool movingRight;
        private RaycastHit2D hitRight, hitLeft;
        private enum MoveDir { RIGHT, LEFT }
        private Rigidbody2D body;
        private BoxCollider2D box2d;
        private Vector2 pos;
        private float distance;
        // Use this for initialization
        void Start()
        {
            body = this.gameObject.GetComponent<Rigidbody2D>();
            box2d = this.gameObject.GetComponent<BoxCollider2D>();
            pos = this.gameObject.transform.position;
            distance = (box2d.size.x / 2) + 0.1f;
        }

        // Update is called once per frame
        void Update()
        {
            CheckForCollision();
            Move();
        }

        private void Move()
        {
            if(movingRight)
                pos = Vector2.MoveTowards(pos, pos + new Vector2(1, 0), Time.deltaTime * speed); //move right
            else
                pos = Vector2.MoveTowards(pos, pos + new Vector2(-1, 0), Time.deltaTime * speed); //move right
            transform.position = pos;
        }

        private void CheckForCollision()
        {
            hitRight = Physics2D.Raycast(transform.position, Vector2.right, distance);
            if (hitRight)
                movingRight = false;
            hitLeft = Physics2D.Raycast(transform.position, Vector2.left, distance);
            if (hitLeft)
                movingRight = true;
            Debug.DrawLine(transform.position, transform.position + new Vector3(distance, 0, 0));
        }
    }
}