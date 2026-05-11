using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tools_PhysicsHandler : MonoBehaviour
{
    public float force = 800f;
    [Space]
    public bool selected = false;
    public bool is_being_used = false;
    private Player_Pickup pickup;
    private RaycastHit hit;
    private Animator animator;
    public GameObject ray;
    public Transform point;


    private GameObject ray2;
    private Constraints_Rope rope;
    

    private void Start()
    {
        //find player pickup
        pickup = FindFirstObjectByType<Player_Pickup>();
        //animator init
        animator = GetComponentInChildren<Animator>();
        //instantiate visual ray and get ray controller (rope constraint works)
        ray2 = Instantiate(ray, point.position, Quaternion.identity);
        rope = ray2.GetComponent<Constraints_Rope>();
        ray2.SetActive(false);
    }

    private void Update()
    {
        //if holding via tool
        if (Player_Pickup.holding && Player_Pickup.using_tool)
        {
            //start moving claw things (start animation)
            animator.SetBool("holding", true);
            Player_Pickup.using_tool = true;
            //enable ray and set end object to held object from interactor
            ray2.SetActive(true);
            //set start object on ray (origin) and disable ray object
            rope.start_object = point.gameObject.transform;
            rope.end_object = pickup.current.transform;
        }
        else
        {
            //exit animation if not holding
            animator.SetBool("holding", false);
            Player_Pickup.using_tool = true;
            //disable ray
            ray2.SetActive(false);
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
