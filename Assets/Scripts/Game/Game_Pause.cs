using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Pause : MonoBehaviour
{
    public GameObject pause_screen;
    public bool is_paused = false;

    private void Update()
    {
        //if esc key pressed, toggle pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            is_paused = !is_paused;
        }
        if (!is_paused)
        {
            //disable pause screen and resume
            pause_screen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            //enable pause screen and pause time
            pause_screen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
