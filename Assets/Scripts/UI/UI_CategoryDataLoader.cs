using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_CategoryDataLoader : MonoBehaviour
{
    public GameObject entry_prefab;
    public UI_CategoryData data;

    void Start()
    {
        foreach (Object_SpawnEntityEntry entities in data.entities)
        {
            //create entry in the list
            GameObject entry = Instantiate(entry_prefab, transform);
            Object_MenuEntityEntry menu_entry = entry.GetComponent<Object_MenuEntityEntry>();
            menu_entry.data = entities;
        }
    }
}
