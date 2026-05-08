using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_ToolPickup : MonoBehaviour
{
    //tool entry/data
    public Tools_Entry entry;
    private Tools_Manager tools_manager;
    private UI_HotBarManager hotbar_manager;

    private void Start()
    {
        //initialize
        tools_manager = FindFirstObjectByType<Tools_Manager>();
        hotbar_manager = FindFirstObjectByType<UI_HotBarManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        //check if player
        if (other.transform.tag == "Player")
        {
            //if tool is not in tool manager
            if (!tools_manager.tools_list.Contains(entry))
            {
                //call addtool
                tools_manager.AddTool(entry);
                //find undo system
                Game_UndoSystem undo_system = FindAnyObjectByType<Game_UndoSystem>();
                //remove me from undo system
                undo_system.objects.Remove(gameObject);
                //add to hotbar
                hotbar_manager.AddTool(entry);
                //destroy the pickup object
                Destroy(gameObject);
            }
        }
    }
}
