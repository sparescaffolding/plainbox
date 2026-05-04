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

    private void Start()
    {
        //find player interactor
        player_interactor = FindFirstObjectByType<Player_Interactor>();
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
