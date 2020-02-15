using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandOnTo : MonoBehaviour
{
    public float QTETime = 1f;

    private GameObject player;
    private float timeCnt;
    private bool isQTE;
    public GameObject QTEHint;


    void Update()
    {
        if (!player) player = GameObject.FindWithTag("Player");

        if(isQTE)
        {
            // if isOver
            if (timeCnt + QTETime <= Time.unscaledTime)
            {
                isQTE = false;
                Recorder.isQTEJump = false;
                Time.timeScale = 1;
                QTEHint.SetActive(false);
            }

            // detect Input (Jump Action)
            if (Input.GetButtonDown("Jump"))
            {
                isQTE = false;
                Recorder.isQTEJump = false;
                Time.timeScale = 1;
                QTEHint.SetActive(false);
                player.GetComponent<PlayerController>().Jump();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Foot" && collision.transform.parent.GetComponent<Rigidbody2D>().velocity.y<=0) 
        {
            // only when player is right above it
            Vector2 me = new Vector2(transform.position.x, transform.position.y);
            Vector2 aboveMe = new Vector2(me.x, me.y + 0.3f);
            

            //RaycastHit2D hit = Physics2D.Raycast(me, aboveMe);
            //Debug.DrawLine(me, aboveMe,Color
            //    .red);
            //if (hit && hit.collider.tag == "Foot")
            //{
            //    Debug.Log(hit.collider.gameObject.name);
                isQTE = true;
                Recorder.isQTEJump = true;
                timeCnt = Time.unscaledTime;
            QTEHint.SetActive(true);
            Time.timeScale = 0;
                // TODO: Show QTE Hint
                //Debug.Log("Press Jump to Jump!");
            //}
        }
    }
}
