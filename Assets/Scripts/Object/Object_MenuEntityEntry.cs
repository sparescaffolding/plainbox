using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Object_MenuEntityEntry : MonoBehaviour
{
    public Object_SpawnEntityEntry data;
    [Space]
    //ui fields
    public TextMeshProUGUI name;
    //object spawning controller/manager
    private Player_ObjectSpawning s;
    private void Start()
    {
        s = FindObjectOfType<Player_ObjectSpawning>();
        //load in data
        name.text = data.name;
    }
    
    public void SpawnObject() 
    {
        s.SpawnObject(data);
    }
}
