using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player_Pickup : MonoBehaviour
{
    public GameObject player;
    public Transform hold_point;
    [Space]
    public float throw_force = 500f;
    public float range;
    public float rotation_sensitivity = 1f;
    public Rigidbody held_object_rigidbody;
    public GameObject held_object;
    public bool can_drop = true;
    private int p_layer;
    [Space]
    public Player_Interactor i;

    private void Start()
    {
        //set layer to Hold
        p_layer = LayerMask.NameToLayer("Hold");
    }

    private void Update()
    {
        if (i.interacting == true)
        {
            if (i.hit.transform.gameObject.CompareTag("Pickup"))
            {
                Pickup(i.hit.transform.gameObject);
            }
            else
            {
                Debug.Log("nothing to pickup");
            }
        }

        if (i.interacting == false)
        {
            Drop();
        }

        if (held_object != null)
        {
            Move();
        }
        else
        {
            return;
        }

        if (i.interacting == true && Input.GetMouseButton(0))
        {
            i.interacting = false;
            Throw();
        } 
    }

    private void Pickup(GameObject pickup)
    {
        if (pickup.GetComponent<Rigidbody>())
        {
            held_object = pickup;       //assign the object that got picked up to held_object
            held_object_rigidbody = pickup.GetComponent<Rigidbody>();       //assign rigidbody
            held_object_rigidbody.isKinematic = true;                       //set to kinematic
            held_object_rigidbody.transform.parent = hold_point.transform;//change parent to held point
            held_object.layer = p_layer;                                   //change layer to modify stuff in rendering (remove visual clipping)
            Physics.IgnoreCollision(pickup.GetComponent<Collider>(), player.GetComponent<Collider>(), true); //dont let object touch player
        }
    }

    private void Drop()
    {
        //let object touch player
        Physics.IgnoreCollision(held_object.GetComponent<Collider>(), player.GetComponent<Collider>(), false);
        held_object.layer = 0; //go back to default/
        held_object_rigidbody.isKinematic = false;//apply gravity
        held_object.transform.parent = null;    //make it parentless
        held_object = null;                     //forget held object
    }

    private void Move()
    {
        held_object.transform.position = hold_point.transform.position; //fix position
        held_object.transform.LookAt(player.transform.position);        //look at player
    }

    private void Throw()
    {
        Drop(); //drop object and apply forward force 
        held_object_rigidbody.AddForce(transform.forward * throw_force);
    }
}
