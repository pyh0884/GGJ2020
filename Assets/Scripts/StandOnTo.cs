using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandOnTo : MonoBehaviour
{
    private GameObject player;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().onOthers = true;
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
            player.GetComponent<PlayerController>().onOthers = false;
        }


    }
    void Update()
    {
        if (!player) player = GameObject.FindWithTag("Player");

    }
}
