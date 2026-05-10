using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_UndoSystem : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<FixedJoint> joints = new List<FixedJoint>();
    public List<SpringJoint> springjoints = new List<SpringJoint>();
    public List<Constraints_Rope> rope_constraint = new List<Constraints_Rope>();
    public GameObject action_list_content;
    public GameObject action_entry;
    //list of actions
    public List<string> lastaction = new List<string>();
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            string last = lastaction[^1];
            lastaction.RemoveAt(lastaction.Count - 1);
            
            //if last action is rope
            if (last == "rope")
            {
                for (int spring = springjoints.Count - 1; spring >= 0; spring--)
                {
                    //remove spring joint
                    SpringJoint springjoint = springjoints[spring];
                    springjoints.RemoveAt(spring);
                    Destroy(springjoint);
                    break;
                }
                for (int rope = rope_constraint.Count - 1; rope >= 0; rope--)
                {
                    //set constraint reference
                    Constraints_Rope constraint = rope_constraint[rope];
                    if (constraint != null)
                    {
                        //initiate action entry
                        GameObject rope_entry = Instantiate(action_entry, action_list_content.transform);
                        UI_ActionEntry rope_ui_action_entry = rope_entry.GetComponent<UI_ActionEntry>();
                        //set action entry text
                        rope_ui_action_entry.text.text = "Rope Undoed";
                        //remove rope
                        rope_constraint.RemoveAt(rope);
                        Destroy(constraint.gameObject);
                        break;
                    }
                }
            }
            //if last action is weld
            else if (last == "weld")
            {
                for (int joint = joints.Count - 1; joint >= 0; joint--)
                {
                    //referene fixedjoint
                    FixedJoint fixedjoint = joints[joint];
                    if (joint != null)
                    {
                        //initiate action entry
                        GameObject w_entry = Instantiate(action_entry, action_list_content.transform);
                        UI_ActionEntry w_ui_action_entry = w_entry.GetComponent<UI_ActionEntry>();
                        //set action entry text
                        w_ui_action_entry.text.text = "Welding Undoed";
                        //remove rope
                        joints.RemoveAt(joint);
                        Destroy(fixedjoint);
                        break;
                    }
                }
            }
            //if object then
            else if (last == "object")
            {
                //initiate action entry
                GameObject entry = Instantiate(action_entry, action_list_content.transform);
                UI_ActionEntry ui_action_entry = entry.GetComponent<UI_ActionEntry>();
                //set action entry text
                ui_action_entry.text.text = "Undoed " + objects[^1].name.Replace("(Clone)", "").Trim();
                //destroy and remove object
                Destroy(objects[^1]);
                objects.RemoveAt(objects.Count - 1);
                Debug.Log("Undo! " + objects.Count);
            }

            last = "";
        }
    }
}