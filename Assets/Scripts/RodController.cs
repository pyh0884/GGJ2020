using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{
    public float DestroyDelayTime = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeadBody")
        {
            transform.parent.GetComponent<CeilingLamp>().canDrop = true;
            transform.parent = null;
            // delay a few seconds to destroy
            Destroy(gameObject, DestroyDelayTime);
        }
    }
}
