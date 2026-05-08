using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HotBarManager : MonoBehaviour
{
    public int tools_count;                                 //amount of tools allowed in hotbar
    public Tools_Manager tools_manager;                     //tools manager to read from 
    public GameObject hotbar_entry;                         //hotbar entry prefab to use as template
    public List<GameObject> hotbar_list =  new List<GameObject>();  //list out hotbar entries
    private void Start()
    {
        //get tools manager
        tools_manager = FindFirstObjectByType<Tools_Manager>();
        //create entries for existing tools
        for (int i = 0; i < tools_manager.tools_list.Count; i++)
        {
            AddTool(tools_manager.tools_list[i]);
        }
    }

    public void AddTool(Tools_Entry toolentry)
    {
        //check if exceeding limit first
        if (hotbar_list.Count >= tools_count)
        {
            return;
        }
        
        GameObject entry = Instantiate(hotbar_entry, transform);
        //get entry component and fill in
        entry.GetComponent<UI_HotBarEntry>().tool = toolentry;
        //store entry
        hotbar_list.Add(entry);
    }
}
