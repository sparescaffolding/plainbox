using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_HealthTrigger : MonoBehaviour
{
    public int amount;

    private void OnTriggerEnter(Collider other)
    {
        //of ontriggerentering player
        if (other.tag == "Player")
        {
            //get controller
            Player_Controller p = other.GetComponent<Player_Controller>();
            //add health
            p.PlayerTakeHealth(amount);
        }
    }
}
