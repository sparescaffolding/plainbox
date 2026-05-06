using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public Player_Controller controller;
    public Player_Camera camera;
    public Animator camera_anim;
    public GameObject fade;
    public GameObject hud;
    public GameObject tools_parent;

    private void Start()
    {
        controller = FindFirstObjectByType<Player_Controller>();
        camera = FindFirstObjectByType<Player_Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Respawn();
        }
    }

    void Respawn()
    {
        controller.health = 100;                                        //reset health
        controller.dead = false;                                        //disable dead bool
        controller.can_move = true;                                     //reenable movement
        camera.can_look = true;                                         //reenable camera look
        camera_anim.enabled = false;                                    //stop animator component
        camera_anim.gameObject.transform.localPosition = Vector3.zero;  //zero the position
        fade.SetActive(false);                                          //disable fade
        hud.SetActive(true);                                            //reenable hud
        tools_parent.SetActive(true);                                   //reenable tools
    }
}
