using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools_Manager : MonoBehaviour
{
    public List<Tools_Entry> tools_list = new List<Tools_Entry>();
    public List<GameObject> tools = new List<GameObject>();
    [Space]
    public int current_tool_id;     //current selected
    public int current_tool_count;       //total tool amount
    [Space]
    public GameObject player;
    public float cooldown = 0.1f;   //time between switching
    private float last_scroll_time;
    private bool hide;
    [Space]
    public bool is_using = false;
    public GameObject tool_held;
    public bool initialized_items = false;
    private int last_tool_id = -1;
    
    void Start()
    {
        //instantiate each entry from tools list as child
        foreach (Tools_Entry g in tools_list)
        {
            GameObject b = Instantiate(g.prefab, transform);
            tools.Add(b);
            initialized_items = true;
        }
        current_tool_count = tools_list.Count;
    }

    void Update()
    {
        //scroll wheel
        float scroll = Mathf.RoundToInt(Input.mouseScrollDelta.y);
        //apply cooldown
        if (Time.time - last_scroll_time > cooldown)
        {
            if (!is_using)
            {
                if (scroll > 0f)
                {
                    current_tool_id++;
                    last_scroll_time = Time.time;
                }
                else if (scroll < 0f)
                {
                    current_tool_id--;
                    last_scroll_time = Time.time;
                }
            }
        }
        
        //allow reverse selection ( i think thats what its called?) where if i scroll past the amount of tools i go back to the beginning
        if (current_tool_id > current_tool_count - 1)
        {
            current_tool_id = 0;
        }
        //never go to negative
        if (current_tool_id < 0)
        {
            current_tool_id = current_tool_count - 1;
        }
        
        //only update when state is changed rather than preframe
        if (current_tool_id != last_tool_id)
        {
            RefreshToolVisibility();
            last_tool_id = current_tool_id;
        }
        
        //enable the tool that belongs to the id only if hide is false
        if (!hide)
        {
            tool_held = tools[current_tool_id];
            tool_held.SetActive(true);
        }
    }
    
    private void RefreshToolVisibility()
    {
        for (int i = 0; i < tools.Count; i++)
        {
            bool active_tool = (i == current_tool_id && !hide);
            tools[i].SetActive(active_tool);
            if (active_tool) 
            {
                tool_held = tools[i];
            }
        }
    }

    //hide tools
    public void HideTools()
    {
        hide = true;
        //hide all tools by disabling
        foreach (GameObject tool in tools)
        {
            tool.SetActive(false);
        }
    }
    
    //unhide tools and show current
    public void ShowTools()
    {
        hide = false;
        //disable all
        foreach (GameObject tool in tools)
        {
            tool.SetActive(false);
        }
        //and only enable the last used tool
        tools[current_tool_id].SetActive(true);
    }
    
    //scaling fix
    private void LateUpdate()
    {
        transform.localScale = Vector3.one;
    }
}
