using System;
using System.Collections;
using System.Collections.Generic;
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
        //raycast
        bool _hit = Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, interactor.distance);
        if (Input.GetMouseButtonDown(1))
        {
            if (_hit)
            {
                //for now, only on props
                if (hit.transform.gameObject.CompareTag("Pickup"))
                {
                    //set selected object on right click
                    selected_object = hit.transform.gameObject;
                    ui_manager.ManipulateMenuShow();
                    cam.can_look = false;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    //clear
                    selected_object = null;
                    cam.can_look = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
            else
            {
                selected_object = null;
                cam.can_look = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
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
