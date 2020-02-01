using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBody : CharacterController
{
    void Start()
    {
        horizontalDirection = 0;
        InitIt();
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
