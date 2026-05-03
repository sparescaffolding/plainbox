using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools_PhysicsHandler : MonoBehaviour
{
    public bool selected = false;

    private void OnEnable()
    {
        selected = true;
    }

    private void OnDisable()
    {
        selected = false;
    }
}
