using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CategoryManager : MonoBehaviour
{
    public List<GameObject> category_views = new List<GameObject>();
    private bool category_selected = false;
    private UI_Manager ui_manager;

    private void Start()
    {
        //initialize ui manager
        ui_manager = FindFirstObjectByType<UI_Manager>();
        if(!category_selected)
        {
            //automatically load first category
            ChangeCategory(0);
        }
    }

    public void ChangeCategory(int category)
    {
        //for each category that isnt equal to the int, disable
        foreach (GameObject g in category_views)
        {
            g.SetActive(false);
        }
        //enable category of int
        category_views[category].SetActive(true);
        //refresh theme so it cam also apply on the other lists
        ui_manager.RefreshUITheme();
    }
}
