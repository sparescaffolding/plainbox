using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_DamageTrigger : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter(Collider other)
    {
        //if ontriggerentering player
        if (other.tag == "Player")
        {
            //get controller
            Player_Controller p = other.GetComponentInParent<Player_Controller>();
            //deduct
            p.PlayerTakeDamage(damage);
        }
    }
}
