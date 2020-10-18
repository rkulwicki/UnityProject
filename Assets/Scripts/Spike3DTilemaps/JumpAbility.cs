using UnityEngine;
using System.Collections;
using static Globals;

//jump relies on "Gravity"
public class JumpAbility : MonoBehaviour
{
    public bool isGravityActive;
    public Vector3 pseudo3DPosition;
    public float speed;
    public float timer;
    public float timeBuffer;
    private bool isJumping;
    // Update is called once per frame
    void Update()
    {
        isGravityActive = this.gameObject.GetComponent<Gravity>().isGravityActive;
        if (Input.GetKeyDown(JumpKey1))
        {
            if (!isGravityActive) { //aka grounded
                isJumping = true;
            }
        }

        if (isJumping)
        {
            timer += Time.deltaTime;
            this.gameObject.GetComponent<Pseudo3DPlayer>().MoveZPositive(speed);
            if (!isGravityActive) 
            {
                if (timer > timeBuffer)//need to get off the ground a little bit
                {
                    isJumping = false;
                    timer = 0f;
                }
            }
        }
    }

    
}
