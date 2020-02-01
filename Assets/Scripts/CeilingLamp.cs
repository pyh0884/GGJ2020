﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLamp : MonoBehaviour
{
    public bool canDrop;
    public float IdealMass = 100f;

    void Start()
    {
        canDrop = false;
    }

    void Update()
    {
        TryDrop();
    }

    private void TryDrop()
    {
        if (canDrop)
        {
            canDrop = false;
            GetComponent<Rigidbody2D>().isKinematic = false;
            GetComponent<Rigidbody2D>().mass = IdealMass;
        }
    }
}