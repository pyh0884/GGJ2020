using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    public Vector3 targetPos;
    public float xOffSet;
    public float yOffSet;
    public float velocity = 20;
    public float velocityX;
    public float velocityY;
    public float velocityZ0;
    private float time = 1;
    private float screenY;
    private float screenZ;
    private float screenX;
    public bool start = false;
    private float timer = 0;
    public Collider2D explosion;
    public float height = 1;
    public float TotalTime = 1.2f;
    public void StartShoot(Vector3 target)
    {
        screenX = transform.position.x;
        screenY = transform.position.y;
        screenZ = 0;
        start = true;
        targetPos = target;
        velocityX = (target.x - transform.position.x) / TotalTime;
        velocityY = (target.y - transform.position.y) / TotalTime;
        velocityZ0 = height / TotalTime + 10 * TotalTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10) 
        {
            start = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
    private void FixedUpdate()
    {
        if (start)
        {
            screenX += velocityX * Time.fixedDeltaTime;
            screenY += velocityY * Time.fixedDeltaTime;
            velocityZ0 -= 20 * Time.fixedDeltaTime;
            screenZ -= velocityZ0 * Time.fixedDeltaTime;
            transform.position = new Vector3((screenX - xOffSet), (screenY - screenZ - yOffSet), 0);
        }
        if (Mathf.Abs(transform.position.x-targetPos.x)<0.5f&& Mathf.Abs(transform.position.y - targetPos.y) < 0.5f) 
        { 
            start = false;
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }
}
