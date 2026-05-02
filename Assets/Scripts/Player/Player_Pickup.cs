using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Pickup : MonoBehaviour
{
    public GameObject hold_point;
    public LayerMask pickup_layer;
    public Player_Interactor interactor;
    private Object_Pickupable current;
    public bool holding = false;
    private void Start()
    {
        interactor = FindFirstObjectByType<Player_Interactor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !holding)
        {
            if (current == null)
            {
                //shoot a raycast with a distance according to the interactor
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactor.distance, pickup_layer))
                {
                    //if object has the pickupable class
                    if (hit.transform.TryGetComponent(out current))
                    {
                        //start grabbing
                        current.Grab(hold_point);
                        holding = true;
                    }
                }
            }
            else
            {
                //drop and clear
                current.Drop();
                current = null;
                holding = false;
            }
        }

        if (Input.GetMouseButton(0) && holding)
        {
            //throw the object thats being held
            current.Throw();
            current = null;
            holding = false;
        }
    }
}
