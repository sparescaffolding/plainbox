using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_UndoSystem : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<FixedJoint> joints = new List<FixedJoint>();
    public GameObject action_list_content;
    public GameObject action_entry;
    
    private void Update()
    {
        //if z pressed
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //check joints list first
            foreach (FixedJoint joint in joints)
            {
                //if there is a joint in joints
                if (joint != null && (joint.gameObject == objects[^1] || joint.connectedBody != null && joint.connectedBody.gameObject == objects[^1]))
                {
                    GameObject entry = Instantiate(action_entry, action_list_content.transform);
                    UI_ActionEntry ui_action_entry = entry.GetComponent<UI_ActionEntry>();
                    //add undo weld text
                    ui_action_entry.text.text = "Welding Undoed";
                    //remove from list
                    joints.Remove(joint);
                    //destroy
                    Destroy(joint);
                }
            }
            
            //destroy
            Destroy(objects[^1]);
            //add action entry and load TMP text reference
            GameObject entry = Instantiate(action_entry, action_list_content.transform);
            UI_ActionEntry ui_action_entry = entry.GetComponent<UI_ActionEntry>();
            //add undo text
            ui_action_entry.text.text = "Undoed " + objects[^1].name.Replace("(Clone)", "").Trim();;
            //remove latest entry
            objects.RemoveAt(objects.Count - 1);
            Debug.Log("Undo! " + objects.Count);
        }
    }
}
