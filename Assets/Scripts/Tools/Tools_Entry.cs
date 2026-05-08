using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool Entry", menuName = "Plainbox/Tool Entry")]
public class Tools_Entry : ScriptableObject
{
    public string name = "Tool";
    public GameObject prefab;
    public GameObject prefab_drop;
    public Texture hotbar_icon;
}
