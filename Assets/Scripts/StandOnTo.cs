using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandOnTo : MonoBehaviour
{
    public float QTETime = 1f;

    private GameObject player;
    private float timeCnt;
    private bool isQTE;


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
            }

            // detect Input (Jump Action)
            if (Input.GetButtonDown("Jump"))
            {
                isQTE = false;
                Recorder.isQTEJump = false;
                Time.timeScale = 1;
                player.GetComponent<PlayerController>().Jump();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // only when player is right above it
            Vector2 me = new Vector2(transform.position.x, transform.position.y + 0.4f);
            Vector2 aboveMe = new Vector2(me.x, me.y + 0.2f);
            

            RaycastHit2D hit = Physics2D.Raycast(me, aboveMe);
            if (hit && hit.collider.tag == "Player")
            {
                Debug.Log(hit.collider.gameObject.name);
                isQTE = true;
                Recorder.isQTEJump = true;
                timeCnt = Time.unscaledTime;
                Time.timeScale = 0;
                // TODO: Show QTE Hint
                Debug.Log("Press Jump to Jump!");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            // 不太懂下面这一坨啥用意 先注释掉了
            /*
            if (gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.x > 0.5f)
            {
                player.GetComponent<PlayerController>().Connected = true;
                player.GetComponent<Rigidbody2D>().velocity = gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
            }
            */
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().Connected = false;
        }


    }

}
