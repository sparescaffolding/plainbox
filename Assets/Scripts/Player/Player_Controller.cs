using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public enum Player_MovementState
{
    walk,
    run,
    crouch,
    air
}

public class Player_Controller : MonoBehaviour
{
    public Player_MovementState movement_state;
    //move and jump speeds/forces
    private float currentspeed;                         //the speed which gets applied
    public float walkspeed;
    public float runspeed;
    public float jumpspeed;
    public float airmultiplier;
    public float drag;
    //crouching
    [Space]
    public float crouchscale;
    private float normalscale;
    
    
    public Transform orientation;
    
    //check stuff (grounded? etc.)
    public float playerheight;
    public LayerMask groundmask;
    
    //bools
    public bool grounded;
    public bool canjump = true;
    public bool exitingslope;
    
    
    //input stuff
    private float horizontal_input;
    private float vertical_input;

    private Vector3 movedirection;                      //the direction the player is moving towards
    private Rigidbody rigidbody;

    private RaycastHit hit_slope;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        //freeze all rotations
        rigidbody.freezeRotation = true;
        //set the player height
        normalscale = transform.localScale.y;
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
        PlayerMovementStateHandle();
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
            //reset jump and start exiting slope
            canjump = false;
            PlayerJump();
            //delay the next jump by a quarter of a second
            Invoke(nameof(PlayerJumpReset), 0.25f);
        }
        
        //start player crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //set the player Y scale to crouch scale
            transform.localScale = new Vector3(transform.localScale.x, crouchscale, transform.localScale.z);
            //push player down towards ground, so that way theres no space below player when initializing crouch
            rigidbody.AddForce(Vector3.down * 10f, ForceMode.Impulse);
        }
        //end player crouching
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            //set the player Y scale back to original scale
            transform.localScale = new Vector3(transform.localScale.x, normalscale, transform.localScale.z);
        }
    }

    public void PlayerMovementStateHandle()
    {
        //running speed
        if (Input.GetKey(KeyCode.LeftShift) && grounded)
        {
            movement_state = Player_MovementState.run;
            currentspeed = runspeed;
        }
        //normal walk speed
        else if (grounded)
        {
            movement_state = Player_MovementState.walk;
            currentspeed = walkspeed;
        }
        else
        {
            movement_state = Player_MovementState.air;
        }
        
        //crouching
        if (Input.GetKey(KeyCode.LeftControl))
        {
            movement_state = Player_MovementState.crouch;
            currentspeed = walkspeed / 2;
        }
    }
    
    private void PlayerMove()
    {
        //calculate movement direction based on orientation direction
        movedirection = orientation.forward * vertical_input + orientation.right * horizontal_input;
        
        //apply player movement force
        if (grounded)
        {
            rigidbody.AddForce(movedirection.normalized * currentspeed * 10, ForceMode.Force);
        }
        //if in the air, apply the air multiplier
        else
        {
            rigidbody.AddForce(movedirection.normalized * currentspeed * 10 * airmultiplier, ForceMode.Force);
        }
        
        //if the player is on a slope angle (angle amount in accordance to max_slope in PlayerSlope()
        if (PlayerSlope() && !exitingslope && movedirection.magnitude > 0.1f && grounded)
        {
            rigidbody.AddForce(PlayerCalculateSlopeDirection() * currentspeed * 20f, ForceMode.Force);
        }
        
        //disable gravity if on a slope
        rigidbody.useGravity = !PlayerSlope() || exitingslope;
    }

    private void PlayerSpeedControl()
    {
        //control speed when on slope
        if (PlayerSlope() && !exitingslope && grounded)
        {
            if (rigidbody.velocity.magnitude > currentspeed)
            {
                //mimic regular currentspeed when going up or down a slope
                Vector3 vel = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
                if (vel.magnitude > currentspeed)
                {
                    Vector3 vel2 = vel.normalized * currentspeed;
                    rigidbody.velocity = new Vector3(vel2.x, rigidbody.velocity.y, vel2.z);
                }
            }
        }
        //control the players speed on ground or air
        else if (!exitingslope)
        {
            //limit velocity when going faster than movespeed
            Vector3 flat_velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            if (flat_velocity.magnitude > currentspeed) {
                //calculate limited speed
                Vector3 limited_velocity = flat_velocity.normalized * currentspeed;
                //apply
                rigidbody.velocity = new Vector3(limited_velocity.x, rigidbody.velocity.y, limited_velocity.z);
            }
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

    private bool PlayerSlope()
    {
        //max angle at which slope handling would be handled at
        float max_slope = 40;
        if (Physics.Raycast(transform.position, Vector3.down, out hit_slope, playerheight * 0.5f + 0.2f))
        {
            float angle = Vector3.Angle(Vector3.up, hit_slope.normal);
            return angle < max_slope;
        }

        return false;
    }

    private Vector3 PlayerCalculateSlopeDirection()
    {
        return Vector3.ProjectOnPlane(movedirection, hit_slope.normal).normalized;
    }
}
