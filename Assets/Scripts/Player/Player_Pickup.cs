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
    public Tools_Manager tools_manager;
    [Range(1.5f, 4f)]
    public float default_distance = 2f;
    public bool using_tool;
    public bool holding = false;
    private void Start()
    {
        interactor = FindFirstObjectByType<Player_Interactor>();
        p = FindFirstObjectByType<Player_PickupDistance>();
        camera = FindFirstObjectByType<Player_Camera>();
        tools_manager = FindObjectOfType<Tools_Manager>();
    }

    private void Update()
    {
        if (current != null)
        {
            //if holding an object and using tool, add ability to rotate
            if (using_tool)
            {
                Rotate();
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && holding && using_tool)
        {
            //freeze object on F key
            Freeze();
        }
        
        if (Input.GetMouseButton(0) && holding)
        {
            //throw the object thats being held
            current.Throw(controller);
            current = null;
            using_tool = false;
            tools_manager.ShowTools();
            p.value = default_distance;
            holding = false;
        }
    }

    public void Freeze()
    {
        //toggle kinematic
        current.r.isKinematic = !current.r.isKinematic;
        //drop and clear
        current.Drop(controller);
        tools_manager.ShowTools();
        using_tool = false;
        camera.can_look = true;
        current = null;
        p.value = default_distance;
        holding = false;
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
                    tools_manager.is_using = true;
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
            tools_manager.is_using = false;
            tools_manager.ShowTools();
            using_tool = false;
            current = null;
            p.value = default_distance;
            holding = false;
        }
    }
}
