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
    public Transform droppoint;
    public GameObject player;
    public float cooldown = 0.1f;   //time between switching
    private float last_scroll_time;
    private bool hide;
    [Space]
    public bool is_using = false;
    public GameObject tool_held;
    public bool initialized_items = false;
    private int last_tool_id = -1;
    private Player_Interactor interactor;
    
    void Start()
    {
        interactor =  FindFirstObjectByType<Player_Interactor>();
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
            if (!is_using && !interactor.pickup.holding)
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

        if (Input.GetKeyDown(KeyCode.G))
        {
            //drop
            RemoveTool();
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
    
    //add tool
    public void AddTool(Tools_Entry tool)
    {
        //check if it exists
        if (tools_list.Contains(tool))
        {
            Debug.Log(tool.name + " is already in the tool list so it will not be added. returning");
            return;
        }
        
        //add to list
        tools_list.Add(tool);
        GameObject tool_obj = Instantiate(tool.prefab, transform);
        tools.Add(tool_obj);
        //instantiate
        //add to tool count
        current_tool_count++;
        RefreshToolVisibility();
    }
    
    //drop tool
    public void RemoveTool()
    {
        Tools_Entry tool = tools_list[current_tool_id];
        //make sure its not a undroppable item
        if (tool.name == "Physics Handler"/*|| tool.name == "put name of undroppable here"*/)
        {
            return;
        }
        else
        {
            //get prefab and rigidbody
            GameObject drop_obj = Instantiate(tool.prefab_drop, droppoint.position, Quaternion.identity);
            Rigidbody r = drop_obj.GetComponent<Rigidbody>();
            //remove from list and destroy
            Destroy(tools[current_tool_id]);
            tools.RemoveAt(current_tool_id);
            tools_list.RemoveAt(current_tool_id);
            //throw
            r.AddForce(droppoint.forward * 300f);
            //deduct from values
            current_tool_count--;
            current_tool_id--;
        }
    }
}
