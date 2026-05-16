using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CategoryDataLoader : MonoBehaviour
{
    public GameObject entry_prefab;
    public UI_CategoryData data;
    private UI_Manager ui_manager;
    
    void Start()
    {
        //get ui manager
        ui_manager  = FindObjectOfType<UI_Manager>();
        foreach (Object_SpawnEntityEntry entities in data.entities)
        {
            //create and fill entry in the list
            GameObject entry = Instantiate(entry_prefab, transform);
            Object_MenuEntityEntry menu_entry = entry.GetComponent<Object_MenuEntityEntry>();
            menu_entry.data = entities;
            //refresh theme so it cam also apply on the other lists
            ui_manager.RefreshUITheme();
        }
    }
}
