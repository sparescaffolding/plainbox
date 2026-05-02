using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_3DPointer : MonoBehaviour
{
    public Camera camera;
    public float range = 10f;
    public bool can_update = true;
    private void Update()
    {
        //hit point is where cursor is pointing
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            //go to cursor point
            if (can_update)
            {
                transform.position = hit.point;
            }
        }
    }
}
