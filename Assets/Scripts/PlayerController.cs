using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    #region variables
    Rigidbody2D playerRigidbody2D;
    Animator anim;
    public float fallMultiplier = 1.4f;
    [Header("current speed on X axis")]
    public float speedX;
    [Header("current speed on Y axis")]
    public float speedY;
    [Header("current direction on X axis")]
    public float horizontalDirection;//between -1~1
    private float tempHorizontalDirection;
    //public AudioManager am;
    private Scene scene;
    [Header("distance from the ground")]
    [Range(0, 0.5f)]
    public float distance;
    [Header("start point of the ray")]
    public Transform groundCheck;
    public Transform groundCheck2;
    [Header("ground mask")]
    public LayerMask groundLayer;
    public bool grounded;
    [Header("start point of the ray")]
    public Transform WallCheck;
    public LayerMask WallLayer;
    public bool HitWall;
    public GameObject GameOver;
    public bool controllable = true;
    #endregion
    [Header("Speed")]
    public float HorizontalSpeed;
    public float jumpSpeed;
    Vector3 LeftDirection = new Vector3(0, 180, 0);
    Vector3 RightDirection = new Vector3(0, 0, 0);
    public Vector3 currentSpeed;
    public bool canTurnAround;
    private bool isPressing = false;
    GameManager gm;

    void MovementX()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if (horizontalDirection < 0) horizontalDirection = -1;
        else if (horizontalDirection > 0) horizontalDirection = 1;
        if (Time.timeScale != 0 && controllable)
        {
            if (horizontalDirection != 0 && isGround)
            {
                anim.SetBool("Run", true);
                //am.UnMute("PlayerRun");
            }
            else
            {
                anim.SetBool("Run", false);
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
                if ((isGround) && !jumping) //((isGround || OnBoss) && !jumping)
                    playerRigidbody2D.velocity = new Vector2(currentSpeed.x, 0);
                else
                    playerRigidbody2D.velocity = new Vector2(currentSpeed.x, playerRigidbody2D.velocity.y);
            }
        }

    public bool JumpKey
    {
        get
        {
            return Input.GetButtonDown("Jump");
        }
    }
    bool isGround
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
    RaycastHit2D hitPoint;
    bool isWall
    {
        get
        {
            //Vector2 start = WallCheck.position;           
            //Vector2 end = new Vector2(start.x + horizontalDirection<0? 1.5f:-1.5f, start.y);
            Debug.DrawLine(new Vector2(gameObject.transform.position.x - 0.95f, gameObject.transform.position.y + 0.27f), WallCheck.position, Color.blue);
            //HitWall = Physics2D.Linecast(new Vector2(gameObject.transform.position.x - 0.95f, gameObject.transform.position.y + 0.27f), WallCheck.position, WallLayer);
            hitPoint = Physics2D.Linecast(new Vector2(gameObject.transform.position.x - 0.95f, gameObject.transform.position.y + 0.27f), WallCheck.position, WallLayer);

            //if (HitWall)
            //WallX =Physics2D.Linecast(new Vector2(gameObject.transform.position.x - 0.95f, gameObject.transform.position.y + 0.27f), WallCheck.position, WallLayer).collider.gameObject.transform.position.x;
            HitWall = hitPoint.collider == null ? false : true;
            return HitWall;
        }
    }
    bool isGround2
    {
        get
        {
            Debug.DrawLine(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 0.27f), WallCheck.position, Color.blue);
            return Physics2D.Linecast(new Vector2(gameObject.transform.position.x - 0.95f, gameObject.transform.position.y + 0.27f), WallCheck.position, groundLayer);
        }
    }
    public void die()
    {
        Time.timeScale = 0;
        //GameOver.SetActive(true);
        //CursorVis.SetActive(true);
        //Destroy(gameObject);
    }
    bool jumping;
    void TryJump()
    {
        if (Time.timeScale != 0 && controllable)
        {
            if (isGround)//|| OnBoss)
            {
                anim.SetBool("Land", true);
                //InsDust();
            }
            else
            {
                anim.SetBool("Land", false);
            }
            if ((isGround ) && JumpKey) //((isGround || OnBoss) && JumpKey &&!isAttacking)
            {
                jumping = true;
                anim.SetTrigger("Jump");
                //am.Play("Jump");
                playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x, jumpSpeed);
            }
            if (!isGround && playerRigidbody2D.velocity.y <= 0) //||(isGround&&playerRigidbody2D.velocity.y==0))
            {
                jumping = false;
                anim.SetBool("Fall", true);
                playerRigidbody2D.gravityScale = fallMultiplier;
            }
            else
            {
                anim.SetBool("Fall", false);
                playerRigidbody2D.gravityScale = 1;
            }

        }
    }
    //public void InsDust()
    //{

    //    if (highFallTimer >= 0.25f)
    //    {
    //        am.Play("PlayerLand");
    //        Instantiate(DustEFX, transform.position, Quaternion.Euler(-90, 0, 0));
    //        if (highFallTimer >= 1.2f)
    //        {
    //            st.GetComponent<ShakeTest>().StartVibration(0.05f, 0.05f, 0.1f);
    //        }
    //        highFallTimer = 0;
    //    }
    //}
    public void controllableOn()
    {
        controllable = true;
    }
    public void controllableOff()
    {
        controllable = false;
    }
    void Start()
    {
        //gm = FindObjectOfType<GameManager>();
        jumping = false;
        playerRigidbody2D = GetComponent<Rigidbody2D>();
        controllable = true;
        anim = GetComponent<Animator>();
    }
    private void Awake()
    {
        Time.timeScale = 1;
        //am = FindObjectOfType<AudioManager>();
        //switch (SceneManager.GetActiveScene().buildIndex)
        //{
        //    case 3:
        //        am.UnMute("BGM1");
        //        break;
        //    default:
        //        break;
        //}
    }
    void Update()
    {
        //if (am == null)
        //    am = FindObjectOfType<AudioManager>();

        TryJump();
        //if (isGround)
        //    playerRigidbody2D.gravityScale = 0;
        //else playerRigidbody2D.gravityScale = 1;
    }
    private void FixedUpdate()
    {
        MovementX();
    }
}