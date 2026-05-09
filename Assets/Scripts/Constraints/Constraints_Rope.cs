using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constraints_Rope : MonoBehaviour
{
    public Transform start_object;
    public Transform end_object;

    private LineRenderer line;

    private void Start()
    {
        //get renderer
        line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        //set positions
        line.SetPosition(0, start_object.position);
        line.SetPosition(1, end_object.position);
    }
}
