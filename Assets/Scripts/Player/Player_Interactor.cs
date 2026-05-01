using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interactor : MonoBehaviour
{
    public float distance = 2f;     //how far the object can be interacted from
    public RaycastHit hit;
    public bool interacting = false;
    void Update()
    {
        //start the raycast from the cameras forward direction
        Physics.Raycast(transform.position, transform.forward, out hit, distance);
        if (Input.GetKeyDown(KeyCode.E))
        {
            interacting = !interacting;
        }
    }

    private void OnDrawGizmos()
    {
        //editor gizmo rendering in scene view
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * distance);
    }
}
