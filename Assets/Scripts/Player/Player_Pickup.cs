using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Pickup : MonoBehaviour
{
    public GameObject hold_point;
    public Collider controller;
    public LayerMask pickup_layer;
    public Player_Interactor interactor;
    private Object_Pickupable current;
    private Player_PickupDistance p;
    private Player_Camera camera;
    [Range(1.5f, 4f)]
    public float default_distance = 2f;
    public bool holding = false;
    private void Start()
    {
        interactor = FindFirstObjectByType<Player_Interactor>();
        p = FindFirstObjectByType<Player_PickupDistance>();
        camera = FindFirstObjectByType<Player_Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && holding)
        {
            //throw the object thats being held
            current.Throw(controller);
            current = null;
            p.value = default_distance;
            holding = false;
        }

        if (current != null)
        {
            //if holding an object, add ability to rotate
            Rotate();
        }
    }

    public void Rotate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            //disable looking around in camera
            camera.can_look = false;
            //get rotations
            float x_axis_rot = Input.GetAxis("Mouse X");
            float y_axis_rot = Input.GetAxis("Mouse Y");
            //rotate in accordance of axis
            current.transform.Rotate(Vector3.down, x_axis_rot);
            current.transform.Rotate(Vector3.right, y_axis_rot);
        }
        else
        {
            //enable camera if not
            camera.can_look = true;
        }
    }
    
    public void AttemptPickup()
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
                    current.Grab(hold_point, controller);
                    p.value = default_distance;
                    holding = true;
                }
            }
        }
        else
        {
            //drop and clear
            current.Drop(controller);
            camera.can_look = true;
            current = null;
            p.value = default_distance;
            holding = false;
        }
    }
}
