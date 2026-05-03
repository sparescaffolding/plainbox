using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pickupable : MonoBehaviour, IInteractable
{
    public Rigidbody r;
    private GameObject p;
    private Player_Camera cam;
    private Player_Pickup f;
    private void Awake()
    {
        r = GetComponent<Rigidbody>();
        cam = FindFirstObjectByType<Player_Camera>();
        
    }

    public void Grab(GameObject point, Collider controller)
    {
        //set point and disable gravity
        this.p = point;
        r.useGravity = false;
        r.drag = 5;
        //ignore player collider
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), controller, true);
    }

    public void Drop(Collider controller)
    {
        r.useGravity = true;
        r.isKinematic = false;
        //discard point and reenable gravity
        this.p = null;
        r.drag = 0;
        cam.can_look = true;
        //ignore player collider
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), controller, false);
    }

    public void Throw(Collider controller)
    {
        //throw force
        r.useGravity = true;
        r.isKinematic = false;
        r.AddForce(p.transform.forward * 500f);
        //drop stuff
        //discard point and reenable gravity
        this.p = null;
        r.drag = 0;
        cam.can_look = true;
        //ignore player collider
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), controller, false);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (f.current != null)
        {
            //if collide with player
            if (other.gameObject.transform.CompareTag("Player"))
            {
                //drop and clear
                f.current.Drop(f.controller);
                f.camera.can_look = true;
                f.tools_manager.is_using = false;
                f.tools_manager.ShowTools();
                f.using_tool = false;
                f.current.r.isKinematic = false;
                f.current = null;
                f.p.value = f.default_distance;
                f.holding = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (p != null)
        {
            //move towards position of point smoothly
            Vector3 pos = Vector3.Lerp(transform.position, p.transform.position, Time.deltaTime * 50f);
            r.MovePosition(pos);
        }
    }
    
    //if picked up with interactable key, disable freezing and rotation
    public void Interact(Player_Pickup player)
    {
        player.using_tool = false;
        player.AttemptPickup();
        Debug.Log("picked up");
    }
}
