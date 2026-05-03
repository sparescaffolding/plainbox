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
        //discard point and reenable gravity
        this.p = null;
        r.useGravity = true;
        r.drag = 0;
        cam.can_look = true;
        //ignore player collider
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), controller, false);
    }

    public void Throw(Collider controller)
    {
        //throw force
        r.AddForce(p.transform.forward * 500f);
        //drop stuff
        //discard point and reenable gravity
        this.p = null;
        r.useGravity = true;
        r.drag = 0;
        cam.can_look = true;
        //ignore player collider
        Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), controller, false);
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
