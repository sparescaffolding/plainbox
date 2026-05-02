using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pickupable : MonoBehaviour
{
    private Rigidbody r;
    private GameObject p;
    private void Awake()
    {
        r = GetComponent<Rigidbody>();
    }

    public void Grab(GameObject point)
    {
        //set point and disable gravity
        this.p = point;
        r.useGravity = false;
    }

    public void Drop()
    {
        //discard point and reenable gravity
        this.p = null;
        r.useGravity = true;
    }

    public void Throw()
    {
        //throw force
        r.AddForce(p.transform.forward * 500f);
        Drop();
    }

    private void FixedUpdate()
    {
        if (p != null)
        {
            //move towards position of point smoothly
            Vector3 pos = Vector3.Lerp(transform.position, p.transform.position, Time.deltaTime * 10f);
            r.MovePosition(pos);
        }
    }
}
