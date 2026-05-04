using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Pause : MonoBehaviour
{
    public Player_Camera camera;
    public GameObject pause_screen;
    public List<GameObject> to_disable = new List<GameObject>();
    public static bool is_paused = false;
    private UI_Manager ui_manager;
    private bool u;

    private void Start()
    {
        camera = FindFirstObjectByType<Player_Camera>();
        ui_manager = FindFirstObjectByType<UI_Manager>();
    }

    private void Update()
    {
        //if esc key pressed, toggle pause
        if (Input.GetKeyDown(KeyCode.Escape) && !is_paused)
        {
            is_paused = true;
            foreach (GameObject g in to_disable)
            {
                //disable what has to be disabled
                g.SetActive(false);
            }
            //enable pause screen and pause time
            pause_screen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            Debug.Log("game paused");
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && is_paused)
        {
            is_paused = false;
            pause_screen.SetActive(false);
            Time.timeScale = 1;
            if (!ui_manager.manipulating)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                camera.can_look = true;
            }
            Debug.Log("game unpaused");
        }
    }

    public void Resume()
    {
        is_paused = false;
        pause_screen.SetActive(false);
        Time.timeScale = 1;
        if (!ui_manager.manipulating)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            camera.can_look = true;
        }
        Debug.Log("game unpaused");
    }
}
