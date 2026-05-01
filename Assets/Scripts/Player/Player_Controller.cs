using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float movespeed;
    public float jumpspeed;
    public float airmultiplier;
    public float drag;
    public Transform orientation;
    
    //check stuff (grounded? etc.)
    public float playerheight;
    public LayerMask groundmask;
    
    //bools
    private bool grounded;
    private bool canjump = true;
    
    
    //input stuff
    private float horizontal_input;
    private float vertical_input;

    private Vector3 movedirection;                      //the direction the player is moving towards
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //freeze all rotations
        rigidbody.freezeRotation = true;
    }

    private void Update() {
        //check if player is grounded
        grounded = Physics.Raycast(transform.position, Vector3.down, playerheight * 0.5f + 0.2f, groundmask);
        
        //drag handler
        if (grounded)
        {
            rigidbody.drag = drag;
        }
        else
        {
            rigidbody.drag = 0;
        }
        
        PlayerInput();
        PlayerSpeedControl();
    }

    private void FixedUpdate() {
        PlayerMove();
    }

    private void PlayerInput()
    {
        horizontal_input = Input.GetAxisRaw("Horizontal");
        vertical_input = Input.GetAxisRaw("Vertical");
        
        //jump functionality
        if (Input.GetKey(KeyCode.Space) && canjump && grounded)
        {
            //reset jump
            canjump = false;
            PlayerJump();
            //delay the next jump by a quarter of a second
            Invoke(nameof(PlayerJumpReset), 0.25f);
        }
    }

    private void PlayerMove()
    {
        //calculate movement direction based on orientation direction
        movedirection = orientation.forward * vertical_input + orientation.right * horizontal_input;
        
        //apply player movement force
        if (grounded)
        {
            rigidbody.AddForce(movedirection.normalized * movespeed * 10, ForceMode.Force);
        }
        //if in the air, apply the air multiplier
        else
        {
            rigidbody.AddForce(movedirection.normalized * movespeed * 10 * airmultiplier, ForceMode.Force);
        }
    }

    private void PlayerSpeedControl()
    {
        //limit velocity when going faster than movespeed
        Vector3 flat_velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        if (flat_velocity.magnitude > movespeed)
        {
            //calculate limited speed
            Vector3 limited_velocity = flat_velocity.normalized * movespeed;
            //apply
            rigidbody.velocity = new Vector3(limited_velocity.x, rigidbody.velocity.y, limited_velocity.z);
        }
    }

    private void PlayerJump()
    {
        //reset the players y velocity so jump results are always consistent
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
        //apply jump force
        rigidbody.AddForce(transform.up * jumpspeed, ForceMode.Impulse);
    }

    private void PlayerJumpReset()
    {
        canjump = true;
    }
}
