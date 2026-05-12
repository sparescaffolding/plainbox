using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManipulatePropertyMenu : MonoBehaviour
{
    private UI_ManipulateMenu manipulate_menu;
    public List<Button> buttons = new List<Button>();
    [Header("add")]
    public ColorBlock add;
    [Space]
    [Header("remove")]
    public ColorBlock remove;
    
    private void Start()
    {
        //get manipulate menu
        manipulate_menu = FindObjectOfType<UI_ManipulateMenu>();
        //apply color buttons
        buttons[0].colors = add;
        buttons[1].colors = add;
    }
    
    public void AddProperty(int property_id)
    {
        //enable property based on id
        switch (property_id)
        {
            case 0: manipulate_menu.manipulator.object_properties.damage = !manipulate_menu.manipulator.object_properties.damage;
                if (!manipulate_menu.manipulator.object_properties.damage)
                {
                    buttons[0].colors = add;
                }
                else if (manipulate_menu.manipulator.object_properties.damage)
                {
                    buttons[0].colors = remove;
                }
                break;
            case 1: manipulate_menu.manipulator.object_properties.health = !manipulate_menu.manipulator.object_properties.health;
                if (!manipulate_menu.manipulator.object_properties.health)
                {
                    buttons[1].colors = add;
                }
                else if (manipulate_menu.manipulator.object_properties.health)
                {
                    buttons[1].colors = remove;
                }
                break;
        }
        manipulate_menu.LoadProperties();
    }
}
