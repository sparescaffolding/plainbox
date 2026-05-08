using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HotBarEntry : MonoBehaviour
{
    public UI_HotBarManager manager;
    public RawImage icon;
    public RawImage selected;
    public Tools_Entry tool;

    private void Start()
    {
        //get manager
        manager = GetComponentInParent<UI_HotBarManager>();
        icon.texture = tool.hotbar_icon;
    }

    private void Update()
    {
        //toggle selected indicator depending if tool manager entry equals this objects entry
        if (manager.tools_manager.tool_held_entry == tool)
        {
            selected.gameObject.SetActive(true);
        }
        else
        {
            selected.gameObject.SetActive(false);
        }
        
        //destroy entry if tools_manager no longer contains tool
        if (!manager.tools_manager.tools_list.Contains(tool))
        {
            Destroy(gameObject);                        //destroy
            manager.hotbar_list.Remove(this.gameObject);//remove from list
        }
    }
}
