using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    #region variables
    protected Rigidbody2D playerRigidbody2D;
    protected Animator anim;
    protected float fallMultiplier = 1.4f;
    [Header("current direction on X axis")]
    public float horizontalDirection;//between -1~1
    [Header("distance from the ground")]
    [Range(0, 0.5f)]
    public float distance;
    [Header("start point of the ray")]
    public Transform groundCheck;
    public Transform groundCheck2;
    [Header("ground mask")]
    public LayerMask groundLayer;
    public bool grounded;
    [Header("Speed")]
    public float HorizontalSpeed;
    public float jumpSpeed;
    protected Vector3 LeftDirection = new Vector3(0, 180, 0);
    protected Vector3 RightDirection = new Vector3(0, 0, 0);
    public Vector3 currentSpeed;
    public bool Connected;

    #endregion

    protected bool jumping;



    protected void InitIt()
    {
        jumping = false;
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public bool isGround
    {
        get
        {
            Vector2 start = groundCheck.position;
            Vector2 end = new Vector2(start.x, start.y - distance);
            Debug.DrawLine(start, end, Color.blue);
            Debug.DrawLine(groundCheck2.position, new Vector2(groundCheck2.position.x, groundCheck2.position.y - distance), Color.blue);
            grounded = (Physics2D.Linecast(start, end, groundLayer) || Physics2D.Linecast(groundCheck2.position, new Vector2(groundCheck2.position.x, groundCheck2.position.y - distance), groundLayer));
            return grounded;
        }
    }

    // adjust horizonDireciton
    public void ChangeDirection(int direc)
    {
        //Debug.Log("Move");
        horizontalDirection = direc;
    }

    public void MoveHorizon()
    {
        if (horizontalDirection != 0 && isGround)
        {
            anim.SetBool("isWalking", true);
            //am.UnMute("PlayerRun");
        }
        else
        {
            anim.SetBool("isWalking", false);
            //am.Mute("PlayerRun");
        }
        if (horizontalDirection < 0)
        {
            playerRigidbody2D.transform.eulerAngles = LeftDirection;
        }
        else if (horizontalDirection > 0)
        {
            playerRigidbody2D.transform.eulerAngles = RightDirection;
        }
        currentSpeed = new Vector3(HorizontalSpeed * horizontalDirection, playerRigidbody2D.velocity.y, 0);
        if (isGround && !jumping && (!Connected || horizontalDirection != 0))    //((isGround || OnBoss) && !jumping)
            playerRigidbody2D.velocity = new Vector2(currentSpeed.x, 0);
        else if (!Connected) 
            playerRigidbody2D.velocity = new Vector2(currentSpeed.x, playerRigidbody2D.velocity.y);

    }

    public void Jump()
    {
        //Debug.Log("Jump");
        //anim.SetTrigger("Jump");
        //am.Play("Jump");
        jumping = true;
        playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpSpeed);
    }

    protected void TryFalling()
    {
        if (!isGround && playerRigidbody2D.velocity.y <= 0 /*&& !Connected*/) //||(isGround&&playerRigidbody2D.velocity.y==0))
        {
            jumping = false;
            playerRigidbody2D.gravityScale = fallMultiplier;
        }
        else
        {
            playerRigidbody2D.gravityScale = 1;
        }
    }

}
