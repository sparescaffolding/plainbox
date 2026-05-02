using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Object_MenuEntityEntry : MonoBehaviour
{
    public Object_SpawnEntityEntry data;
    [Space]
    //ui fields
    public RawImage icon;
    public TextMeshProUGUI name;
    //object spawning controller/manager
    private Player_ObjectSpawning s;
    private void Start()
    {
        s = FindObjectOfType<Player_ObjectSpawning>();
        //load in data
        //check if icon is null, if null dont apply data icon but rather fallback from prefab
        if (data.icon != null)
        {
            icon.texture = data.icon;
        }
        //same thing here but with name
        if (data.name != null)
        {
            name.text = data.name;
        }
    }
    
    public void SpawnObject() 
    {
        s.SpawnObject(data);
    }
}
