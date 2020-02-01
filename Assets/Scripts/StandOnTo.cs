using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandOnTo : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity.x > 0.5f)
            {
                player.GetComponent<PlayerController>().Connected = true;
                player.GetComponent<Rigidbody2D>().velocity = gameObject.transform.parent.GetComponent<Rigidbody2D>().velocity;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().Connected = false;

        }

    }
    void Update()
    {
        if (!player) player = GameObject.FindWithTag("Player");

    }
}
