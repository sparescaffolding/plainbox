using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Category Data", menuName = "Plainbox/Category Data")]
public class UI_CategoryData : ScriptableObject
{
    public List<Object_SpawnEntityEntry> entities = new List<Object_SpawnEntityEntry>();
}
