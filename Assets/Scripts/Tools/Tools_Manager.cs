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
    [Space]
    public bool is_using = false;
    
    void Start()
    {
        //instantiate each entry from tools list as child
        foreach (Tools_Entry g in tools_list)
        {
            GameObject b = Instantiate(g.prefab, transform);
            tools.Add(b);
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
        
        //set the active tool to current tool id
        foreach (GameObject tool in tools)
        {
            tool.SetActive(false);
        }
        //enable the tool that belongs to the id
        tools[current_tool_id].SetActive(true);
    }

    //scaling fix
    private void LateUpdate()
    {
        transform.localScale = Vector3.one;
    }
}
