using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ObjectSpawning : MonoBehaviour
{
    public GameObject cursor;
    public float y_offset = 2;
    private Game_UndoSystem undo;

    private void Start()
    {
        undo = FindFirstObjectByType<Game_UndoSystem>();
    }

    public void SpawnObject(Object_SpawnEntityEntry entry)
    {
        //instantiate object from entry at cursor position
        //apply offsetting
        Vector3 spawn_position = cursor.transform.position + Vector3.up * y_offset;
        Instantiate(entry.prefab, spawn_position, entry.prefab.transform.rotation);
        //set last action
        undo.lastaction.Add("object");
    }
}
