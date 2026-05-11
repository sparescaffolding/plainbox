using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ManipulatePropertyMenu : MonoBehaviour
{
    private UI_ManipulateMenu manipulate_menu;
    
    private void Start()
    {
        //get manipulate menu
        manipulate_menu = FindObjectOfType<UI_ManipulateMenu>();
    }
    
    public void AddProperty(int property_id)
    {
        //enable property based on id
        switch (property_id)
        {
            case 0: manipulate_menu.manipulator.object_properties.damage = true; break;
            case 1: manipulate_menu.manipulator.object_properties.health = true; break;
        }
        manipulate_menu.LoadProperties();
    }
}
