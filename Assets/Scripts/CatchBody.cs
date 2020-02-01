using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBody : MonoBehaviour
{
    public GameObject Emit;
    public PlayerController pc;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11) 
        {
            //Debug.Log("trigger");
            if (Input.GetKeyDown(KeyCode.Mouse0)) 
            {
                //FindObjectOfType<Replayer>().GetComponent<Replayer>().EndLife(collision.gameObject);
                var tmp = collision.gameObject.GetComponent<DeadBody>();
                Replayer.ended[tmp.number] = true;
                tmp.ChangeDirection(0);
                pc.Bullet = collision.gameObject;
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                pc.CanShoot = true;
            }
        }
    }
}
