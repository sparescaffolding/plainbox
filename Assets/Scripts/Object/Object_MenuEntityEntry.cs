using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Object_MenuEntityEntry : MonoBehaviour
{
    public Object_SpawnEntityEntry data;
    [Space]
    public TextMeshProUGUI name;
    private void Start()
    {
        name.text = data.name;
    }
}
