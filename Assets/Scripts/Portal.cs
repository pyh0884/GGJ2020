using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Trans
{
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            anim.SetTrigger("FadeOut");
            LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

}
