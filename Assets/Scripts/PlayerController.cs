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
    public bool CanShoot;
    public LineRenderer TrajectoryLine;
    private bool CannonAiming = false;
    private bool CannonPressed = false;
    public GameObject EmitPoint;
    public GameObject Bullet;
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
    private float VelocityZ = 1 / 1.2f + 10 * 1.2f;
    private float VelocityY;
    private float VelocityX;
    public void CannonTrajectory()
    {
        TrajectoryLine.enabled = true;
        TrajectoryLine.SetPosition(0, EmitPoint.transform.position);
        VelocityX = (Camera.main.ScreenToWorldPoint(Input.mousePosition).x - EmitPoint.transform.position.x) / 1.2f;
        VelocityY = (Camera.main.ScreenToWorldPoint(Input.mousePosition).y - EmitPoint.transform.position.y) / 1.2f;
        VelocityZ = 1 / 1.2f + 10 * 1.2f;
        float i = 0.02f;
        while (i < 1 && (Mathf.Abs(EmitPoint.transform.position.x + VelocityX * i * 1.75f - Camera.main.ScreenToWorldPoint(Input.mousePosition).x) > 0.2f || i < 0.65f))
        {
            TrajectoryLine.positionCount = Mathf.RoundToInt(i / 0.02f) + 1;
            TrajectoryLine.SetPosition(Mathf.RoundToInt(i / 0.02f), new Vector3(EmitPoint.transform.position.x + VelocityX * i * 1.75f, EmitPoint.transform.position.y + (VelocityY * i + VelocityZ * i) * 1.95f, 0));
            VelocityZ -= 20 * 0.02f;
            i += 0.02f;
        }
        //for (float i = 0.02f; i < 1.5f; i += 0.02f)
        //{
        //    TrajectoryLine.positionCount = Mathf.RoundToInt(i / 0.02f) + 1;
        //    TrajectoryLine.SetPosition(Mathf.RoundToInt(i / 0.02f), new Vector3(EmitPoint.transform.position.x + VelocityX * i * 1.75f, EmitPoint.transform.position.y + (VelocityY * i + VelocityZ * i) * 1.95f));
        //    VelocityZ -= 20 * 0.02f;
        //}
    }
    public void CannonShoot()
    {
        anim.SetBool("Throw", false);
        CannonPressed = false;
        CannonAiming = false;
        TrajectoryLine.enabled = false;
        Bullet.GetComponent<CannonBullet>().StartShoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        CanShoot = false;
    }
    void CannonUpdate()
    {
        if (CanShoot && Input.GetKey(KeyCode.Mouse0)) 
        {
            CannonPressed = true;
            CannonAiming = true;
            Bullet.transform.position = EmitPoint.transform.position;
        }
        if ((CanShoot && Input.GetKeyUp(KeyCode.Mouse0) && CannonPressed) /*|| (Mp <= 5 && isSniper && pressed)*/)
        {
            CannonShoot();
            am.Play("Throw");
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
        anim.SetTrigger("Die");
        playerRigidbody2D.velocity = Vector2.zero;
        controllable = false;
        respawn();
    }
    public void respawn()
    {
        Time.timeScale = 0;
        gm.AddTurn();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void TryJump()
    {
        if (Time.timeScale != 0 && controllable)
        {
            if ((isGround) && JumpKey )
            {
                jumping = true;
                Jump();
                am.Play("Jump");
            }
            TryFalling();
        }
    }
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        controllable = true;
        InitIt();
    }
    private void Awake()
    {
        Time.timeScale = 1;
        am = FindObjectOfType<AudioManager>();
    }
    void Update()
    {
        CannonUpdate();
        TryJump();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            am.Play("Die");
            die();
        }
    }
    private void FixedUpdate()
    {
        MovementX();
        if (CannonAiming)
        {
            CannonTrajectory();
        }
    }
}