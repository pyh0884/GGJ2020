using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private bool isDown;    // 跟对应陷阱机关的开闭状态对应

    private void Start()
    {
        isDown = false;
    }

    private void Update()
    {
        if (isDown)
        {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDown)
        {
            if (collision.tag == "Player" || collision.tag == "DeadBody")
            {
                isDown = true;
                // Trigger sth...& play anim
                // to test, change color
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "DeadBody")
        {         
            isDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "DeadBody")
        {
            // return to normal status, play anim
            // to test, change color

            RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(transform.position.x, transform.position.y + 0.3f));
            if (hit && (hit.collider.tag == "Player" || hit.collider.tag == "DeadBody"))
            { }
            else
            { isDown = false; }
          
        }
    }

}
