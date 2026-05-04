using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject ui_spawnmenu;
    public UI_ManipulateMenu ui_manipulatemenu;
    [Space] public Player_Controller player_controller;
    public Player_Camera camera;
    public Player_3DPointer cursor;

    private void Start()
    {
        player_controller = FindFirstObjectByType<Player_Controller>(); //player controller to toggle movement
        camera = FindFirstObjectByType<Player_Camera>(); //player camera to toggle looking around
        cursor = FindFirstObjectByType<Player_3DPointer>(); //player pointer to point where to spawn
    }

    public void ManipulateMenuShow()
    {
        ui_manipulatemenu.gameObject.SetActive(true);
        ui_manipulatemenu.Load();
    }

    void Update()
    {
        //spawn menu
        //
        //if tab is held down, show spawn menu, if held up (released), hide
        if (Input.GetKeyDown(KeyCode.Tab) && !Game_Pause.is_paused)
        {
            ui_spawnmenu.SetActive(true); //enable spawn menu
            camera.can_look = false; //disable looking
            Cursor.lockState = CursorLockMode.None; //unlock mouse
            Cursor.visible = true; //make mouse visible
            cursor.can_update = false; //disable 3d pointer
            Debug.Log(ui_spawnmenu.name + " shown");
        }

        if (Input.GetKeyUp(KeyCode.Tab) && !Game_Pause.is_paused)
        {
            ui_spawnmenu.SetActive(false); //disable spawn menu
            camera.can_look = true; //reenable looking
            Cursor.lockState = CursorLockMode.Locked; //lock mouse
            Cursor.visible = false; //make mouse invisible
            cursor.can_update = true; //reenable 3d pointer
            Debug.Log(ui_spawnmenu.name + " hidden");
        }
    }
}
