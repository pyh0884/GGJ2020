using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBody : MonoBehaviour
{
    public GameObject Emit;
    public PlayerController pc;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11|| collision.gameObject.layer == 9) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !pc.CanShoot)
            {
                Debug.Log("trigger");

                transform.parent.GetComponent<Animator>().SetBool("Throw",true);
                //FindObjectOfType<Replayer>().GetComponent<Replayer>().EndLife(collision.gameObject);
                var tmp = collision.gameObject.GetComponent<DeadBody>();
                Replayer.ended[tmp.number] = true;
                if (collision.gameObject.layer == 9)
                {
                    collision.gameObject.AddComponent<CannonBullet>();
                    collision.gameObject.GetComponent<DeadBody>().isDead = true;
                    collision.gameObject.layer = 11;
                    collision.gameObject.GetComponent<Animator>().SetTrigger("Die");
                }
                tmp.ChangeDirection(0);
                pc.Bullet = collision.gameObject;
                collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                pc.CanShoot = true;
            }
        }
    }
}
