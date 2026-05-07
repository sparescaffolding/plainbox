using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    private void Start()
    {
        //initialize
        camera = Camera.main;
        interactor = FindFirstObjectByType<Player_Interactor>();
        ui_manager = FindFirstObjectByType<UI_Manager>();
        cam =  FindFirstObjectByType<Player_Camera>();
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
    }

    private void OnDrawGizmos()
    {
        //editor gizmo rendering in scene view
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(camera.transform.position, camera.transform.forward * interactor.distance);
    }
}
