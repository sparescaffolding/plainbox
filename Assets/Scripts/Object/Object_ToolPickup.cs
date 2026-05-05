using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_ToolPickup : MonoBehaviour
{
    //tool entry/data
    public Tools_Entry entry;
    private Tools_Manager tools_manager;

    private void Start()
    {
        //initialize
        tools_manager = FindFirstObjectByType<Tools_Manager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        //check if player
        if (other.transform.tag == "Player")
        {
            //call addtool
            tools_manager.AddTool(entry);
            //destroy the pickup object
            Destroy(gameObject);
        }
    }
}
