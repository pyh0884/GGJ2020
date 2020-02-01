using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : CharacterController
{

    #region variables
    [Header("General")]
    public AudioManager am;
    private Scene scene;
    public GameObject GameOver;
    public bool controllable = true;
    
    #endregion

    private GameManager gm;

    void MovementX()
    {
        horizontalDirection = Input.GetAxisRaw("Horizontal");
        if (horizontalDirection < 0) horizontalDirection = -1;
        else if (horizontalDirection > 0) horizontalDirection = 1;
        if (Time.timeScale != 0 && controllable) 
        {
            MoveHorizon();
        }
    }

    public bool JumpKey
    {
        get
        {
            return Input.GetButtonDown("Jump");
        }
    }

    public void die()
    {
        Time.timeScale = 0;
        gm.AddTurn();
        // TODO: Start again

        //GameOver.SetActive(true);
        //CursorVis.SetActive(true);
        //Destroy(gameObject);
    }

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
                Jump();
            }

            TryFalling();

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
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        controllable = true;
        InitIt();
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


        // to test recorder, when pressing "Y", the player died.
        ///////// test begin ////////////////
        if (Input.GetKeyDown(KeyCode.Y))
        {
            die();
            SceneManager.LoadScene("1Stage1");
        }

        //////// test end //////////////
    }
    private void FixedUpdate()
    {
        MovementX();
    }
}