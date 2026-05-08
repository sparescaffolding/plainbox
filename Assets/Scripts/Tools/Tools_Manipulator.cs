using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum Constraint
{
    None,
    Weld
}

public class Tools_Manipulator : MonoBehaviour
{
    public Camera camera;
    public Player_Interactor interactor;
    public RaycastHit hit;
    [Space]
    public GameObject selected_object;
    private UI_Manager ui_manager;
    private Player_Camera cam;
    public Object_Manipulatable object_properties;
    [Space]
    public TextMeshProUGUI head;
    public TextMeshProUGUI constraint_name;
    public TextMeshProUGUI constraint_desc;
    [Space]
    [Header("manipulate settings")]
    public Constraint constraint_state;

    [Space] [Header("welding")]
    public GameObject w_first_object;
    public GameObject w_other_object;

    private void Start()
    {
        //initialize
        camera = Camera.main;
        interactor = FindFirstObjectByType<Player_Interactor>();
        ui_manager = FindFirstObjectByType<UI_Manager>();
        cam =  FindFirstObjectByType<Player_Camera>();
        constraint_state = Constraint.None;
    }
    
    void Update()
    {
        //if there is an object selected
        if (selected_object != null)
        {
            string objectname = selected_object.name.Replace("(Clone)", "").Trim();
            //update tite
            head.text = "ENTITY MANIPULATOR\n" + "|" + objectname + "|";
        }
        else
        {
            head.text = "ENTITY MANIPULATOR\n|NO OBJECT SELECTED|";
        }
        
        //constraint screen thing
        if (constraint_state == Constraint.None)
        {
            constraint_name.text = "Constraint: " + "N/A";
            constraint_desc.text = "None:\nNo constraints are selected!";
        }
        else if (constraint_state == Constraint.Weld)
        {
            constraint_name.text = "Constraint: " + "Weld";
            constraint_desc.text = "Select first object you want to weld, Then select another object which you want to weld the first one to.";
        }
        
        //raycast
        bool _hit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interactor.distance);
        if (Input.GetMouseButtonDown(1))
        {
            if (_hit)
            {
                interactor.tools_manager.is_using = true;
                //if object is set so that it can be manipulated by the tool
                if (hit.transform.TryGetComponent<Object_Manipulatable>(out Object_Manipulatable manipulatable))
                {
                    //set what to load available properties
                    object_properties = manipulatable;
                    //set selected object on right click
                    selected_object = hit.transform.gameObject;
                    ui_manager.ManipulateMenuShow();
                    cam.can_look = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    //discord set properties
                    object_properties = null;
                    //clear
                    selected_object = null;
                    cam.can_look = true;
                    interactor.tools_manager.is_using = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    ui_manager.manipulating = false;
                    _hit = false;
                }
            }
            else
            {
                //discord set properties
                object_properties = null;
                selected_object = null;
                cam.can_look = true;
                interactor.tools_manager.is_using = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ui_manager.manipulating = false;
            }
        }
        
        //constraint functions
        Weld();
    }

    public void Weld()
    {
        bool selected_first_obj = false;
    
        if (constraint_state == Constraint.Weld)
        {
            //shoot
            RaycastHit hit;
            bool _hit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interactor.distance);

            if (Input.GetMouseButtonDown(0) && _hit && !UI_Manager.using_ui)
            {
                GameObject clicked = hit.collider.gameObject;

                //if first click set first object
                if (w_first_object == null)
                {
                    w_first_object = clicked;
                    return;
                }
                //else if this is the second click, weld
                else
                {
                    w_other_object = clicked;

                    //set rigidbodies
                    Rigidbody A = w_first_object.GetComponent<Rigidbody>();
                    Rigidbody B = w_other_object.GetComponent<Rigidbody>();

                    if (A != null && B != null)
                    {
                        //add fixed joint component
                        FixedJoint joint = w_first_object.AddComponent<FixedJoint>();
                        joint.connectedBody = B;
                        joint.breakForce = Mathf.Infinity;
                        joint.breakTorque = Mathf.Infinity;
                        joint.enableCollision = false;
                        //finish by adding to undosystem
                        ui_manager.undosystem.joints.Add(joint);    
                    }
                
                    //reset
                    constraint_desc.text = "Select first object you want to weld.";
                    w_first_object = null;
                    w_other_object = null;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //editor gizmo rendering in scene view
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(camera.transform.position, camera.transform.forward * interactor.distance);
    }
}
