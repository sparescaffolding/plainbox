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
    public Transform spawn_position;
    public Game_UndoSystem undo;
    [Space]
    public int max_constraint = 100;
    public int current = 0;
    
    private void Start()
    {
        controller = FindFirstObjectByType<Player_Controller>();
        camera = FindFirstObjectByType<Player_Camera>();
        undo = GetComponent<Game_UndoSystem>();
    }

    private void Update()
    {
        if ((undo.joints.Count + undo.rope_constraint.Count) >= max_constraint)
        {
            undo.max_hit = true;
        }
        else
        {
            undo.max_hit = false;
        }
        
        //calculate total
        current = undo.joints.Count + undo.rope_constraint.Count;
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        controller.health = 100;                                        //reset health
        controller.dead = false;                                        //disable dead bool
        controller.gameObject.transform.localPosition = spawn_position.localPosition;   //respawn position
        controller.can_move = true;                                     //reenable movement
        camera.can_look = true;                                         //reenable camera look
        camera_anim.enabled = false;                                    //stop animator component
        camera_anim.gameObject.transform.localPosition = Vector3.zero;  //zero the position
        fade.SetActive(false);                                          //disable fade
        hud.SetActive(true);                                            //reenable hud
        tools_parent.SetActive(true);                                   //reenable tools
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
