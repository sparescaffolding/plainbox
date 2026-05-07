using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject ui_spawnmenu;
    public GameObject ui_manipulatesettings;
    public UI_ManipulateMenu ui_manipulatemenu;
    public GameObject ui_hud;
    [Space] public Player_Controller player_controller;
    public Player_Camera camera;
    public Player_3DPointer cursor;
    [Space]
    public bool manipulating = false;

    private void Start()
    {
        player_controller = FindFirstObjectByType<Player_Controller>(); //player controller to toggle movement
        camera = FindFirstObjectByType<Player_Camera>(); //player camera to toggle looking around
        cursor = FindFirstObjectByType<Player_3DPointer>(); //player pointer to point where to spawn
        //enable hud
        ui_hud.SetActive(true);
    }

    public void ManipulateMenuShow()
    {
        if (!player_controller.dead)
        {
            ui_manipulatemenu.gameObject.SetActive(true);
            ui_manipulatemenu.Load();
            manipulating = true;
        }
    }
    
    public void ManipulateMenuClose(bool cursor_lock)
    {
        ui_manipulatemenu.gameObject.SetActive(false);
        manipulating = false;
        ui_manipulatemenu.manipulator.selected_object = null;
        camera.can_look = true;
        //disable is_using if exit button used
        ui_manipulatemenu.manipulator.interactor.tools_manager.is_using = false;
        if (cursor_lock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void SpawnMenuShow()
    {
        //make sure this menu cant be opened when dead, paused or manipulating
        if (!player_controller.dead && !Game_Pause.is_paused && !manipulating)
        {
            ui_spawnmenu.SetActive(true); //enable spawn menu
            camera.can_look = false; //disable looking
            Cursor.lockState = CursorLockMode.None; //unlock mouse
            Cursor.visible = true; //make mouse visible
            cursor.can_update = false; //disable 3d pointer
            Debug.Log(ui_spawnmenu.name + " shown");
        }
    }

    public void SpawnMenuClose(bool cursor_lock)
    {
        //cannot open menu is paused or manipulating
        if (!Game_Pause.is_paused && !manipulating)
        {
            ui_spawnmenu.SetActive(false); //disable spawn menu
            camera.can_look = true; //reenable looking
            if (cursor_lock)
            {
                Cursor.lockState = CursorLockMode.Locked; //lock mouse
                Cursor.visible = false; //make mouse invisible
            }
            cursor.can_update = true; //reenable 3d pointer
            Debug.Log(ui_spawnmenu.name + " hidden");
        }
    }

    public void ManipulateSettingsShow()
    {
        ui_manipulatesettings.SetActive(true);
        camera.can_look = false; //disable looking
        Cursor.lockState = CursorLockMode.None; //unlock mouse
        Cursor.visible = true; //make mouse visible
        cursor.can_update = false; //disable 3d pointer
    }

    public void ManipulateSettingsClose(bool cursor_lock)
    {
        ui_manipulatesettings.SetActive(false);
        camera.can_look = true; //reenable looking
        if (cursor_lock)
        {
            Cursor.lockState = CursorLockMode.Locked; //lock mouse
            Cursor.visible = false; //make mouse invisible
        }
        cursor.can_update = true; //reenable 3d pointer
    }

    void Update()
    {
        //if paused close all menu
        if (Game_Pause.is_paused)
        {
            ManipulateSettingsClose(false);
            SpawnMenuClose(false);
            ManipulateMenuClose(false);
        }
        
        //spawn menu
        //
        //if tab is held down, show spawn menu, if held up (released), hide
        if (Input.GetKeyDown(KeyCode.Tab) && !Game_Pause.is_paused && !manipulating)
        {
            SpawnMenuShow();
        }

        if (Input.GetKeyUp(KeyCode.Tab) && !Game_Pause.is_paused && !manipulating)
        {
            SpawnMenuClose(true);
        }
        
        //manipulate settings menu
        //
        //if C is held down, show manipulate settings menu, if held up (released), hide
        if (Input.GetKeyDown(KeyCode.C) && !Game_Pause.is_paused && !manipulating)
        {
            ManipulateSettingsShow();
        }

        if (Input.GetKeyUp(KeyCode.C) && !Game_Pause.is_paused && !manipulating)
        {
            ManipulateSettingsClose(true);
        }
        
        //if player dead disable all UI
        if (player_controller.dead)
        {
            //spawn menu
            SpawnMenuClose(true);
            //manipulate menu
            ManipulateMenuClose(true);
            //disable hud
            ui_hud.SetActive(false);
        }
        
        //disable menus that disable camera movement via mouse if can look
        if (camera.can_look)
        {
            //this is a temporary fix, its very broken because if you just right click especially in the spawn menu it just closes
            ManipulateMenuClose(true);
            SpawnMenuClose(true);
        }
    }
}
