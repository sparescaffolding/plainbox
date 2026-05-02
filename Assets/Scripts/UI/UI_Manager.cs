using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject ui_spawnmenu;
    [Space]
    public Player_Controller player_controller;
    public Player_Camera camera;

    private void Start()
    {
        player_controller = FindFirstObjectByType<Player_Controller>();
        camera = FindFirstObjectByType<Player_Camera>();
    }

    void Update()
    {
        //spawn menu
        //
        //if tab is held down, show spawn menu, if held up (released), hide
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ui_spawnmenu.SetActive(true);               //enable spawn menu
            camera.can_look = false;                    //disable looking
            player_controller.can_move = false;         //disable movement
            Cursor.lockState = CursorLockMode.None;     //unlock mouse
            Cursor.visible = true;                      //make mouse visible
            Debug.Log(ui_spawnmenu.name + " shown");
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ui_spawnmenu.SetActive(false);              //disable spawn menu
            camera.can_look = true;                     //reenable looking
            player_controller.can_move = true;          //reenable movement
            Cursor.lockState = CursorLockMode.Locked;   //lock mouse
            Cursor.visible = false;                     //make mouse invisible
            Debug.Log(ui_spawnmenu.name + " hidden");
        }
    }
}
