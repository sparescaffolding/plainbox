using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tools_PhysicsHandler : MonoBehaviour
{
    public float force = 800f;
    [Space]
    public Camera camera;
    public bool selected = false;
    public bool is_being_used = false;
    private Player_Interactor player_interactor;
    private RaycastHit hit;
    private Animator animator;

    private void Start()
    {
        //find player interactor
        player_interactor = FindFirstObjectByType<Player_Interactor>();
        //animator init
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //if holding via tool
        if (player_interactor.pickup.holding && player_interactor.pickup.using_tool)
        {
            //start moving claw things (start animation)
            animator.SetBool("holding", true);
        }
        else
        {
            //exit animation if not holding
            animator.SetBool("holding", false);
        }
    }

    private void OnEnable()
    {
        selected = true;
    }

    private void OnDisable()
    {
        selected = false;
    }
}
