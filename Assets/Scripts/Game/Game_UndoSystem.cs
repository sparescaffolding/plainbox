using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_UndoSystem : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();

    private void Update()
    {
        //if z pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //destroy
            Destroy(objects[^1]);
            //remove latest entry
            objects.RemoveAt(objects.Count - 1);
            Debug.Log("Undo! " + objects.Count);
        }
    }
}
