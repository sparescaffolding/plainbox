using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interactor : MonoBehaviour
{
    public float distance = 2f;     //how far the object can be interacted from
    public Player_PickupDistance d;
    public RaycastHit hit;
    //
    //stuff that need interaction
    //
    public Player_Pickup pickup;
    //
    public bool interacting = false;
    void Update()
    {
        //this fixes not being able to drop when picking up an item and setting its distance to more than distance
        distance = d.value;
        //start the raycast from the cameras forward direction
        Physics.Raycast(transform.position, transform.forward, out hit, distance);
        if (Input.GetKeyDown(KeyCode.E))
        {
            //use Interact interface
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                //start interacting
                interactable.Interact(pickup);
            }
            else //if looking at nothing/uninteractable
            {
                Debug.Log("nothing to interact");
            }
        }
        //this is for picking up with the main tool (right click)
        if (Input.GetMouseButtonDown(1))
        {
            //use Interact interface
            if (hit.transform.CompareTag("Pickup"))
            {
                //start interacting
                pickup.AttemptPickup();
                Debug.Log("attempted picking up " + hit.transform.name);
            }
            else //if looking at nothing/uninteractable
            {
                Debug.Log("nothing to interact");
            }
        }
    }

    private void OnDrawGizmos()
    {
        //editor gizmo rendering in scene view
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * distance);
    }
}
