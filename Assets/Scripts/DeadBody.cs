using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : CharacterController
{
    //public bool isDead;
    public int number;

    void Start()
    {
        horizontalDirection = 0;
        InitIt();
        //isDead = false;
        
    }

    void Update()
    {
        TryFalling();
    }

    void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            MoveHorizon();
        }
    }


}
