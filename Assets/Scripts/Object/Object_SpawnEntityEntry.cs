using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawn Entity Entry", menuName = "Plainbox/Spawn Entity Entry")]
public class Object_SpawnEntityEntry : ScriptableObject
{
    public string name;         //object name
    public Texture2D icon;      //object icon
    public GameObject prefab;   //object prefab
}
