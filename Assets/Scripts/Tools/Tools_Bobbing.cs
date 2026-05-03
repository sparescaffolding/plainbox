using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools_Bobbing : MonoBehaviour
{
    public Player_Controller controller;
    public float intensity;
    public float intensity_x;
    public float speed;
    [Space]
    private Vector3 start_pos;
    private float sin_time;
    private float bob_fade;
    private float speed_original;
    private float speed_sprint;

    private void Start()
    {
        //set player
        controller = FindFirstObjectByType<Player_Controller>();
        //setup sprint speed by multiplying speed by 2
        speed_sprint = speed * 2;
        //remember initial speed
        speed_original = speed;
        start_pos = transform.localPosition;
    }

    private void Update()
    {   
        sin_time += Time.deltaTime * speed;
        
        float sin_y = -Mathf.Abs(intensity * Mathf.Sin(sin_time)) * bob_fade;
        float sin_x = intensity * Mathf.Cos(sin_time) * intensity_x * bob_fade;

        Vector3 input = new Vector3(Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));

        //only bob when player is grounded and theres input
        if (input.magnitude > 0f && controller.grounded)
        {/*    this is without the fading
            sin_time += Time.deltaTime * speed;*/
            //with fading:
            bob_fade = Mathf.MoveTowards(bob_fade, 1f, Time.deltaTime * 9f);
        }
        else
        {/*     without fading
            sin_time = 0f;*/
            bob_fade = Mathf.MoveTowards(bob_fade, 0f, Time.deltaTime * 9f);
        }

        //if player running, double speeed
        if (controller.movement_state == Player_MovementState.run)
        {
            speed = speed_sprint;
        }
        else
        {
            speed = speed_original;
        }

        Vector3 offset = new Vector3(sin_x, sin_y, 0f);

        transform.localPosition = start_pos + offset;
    }
}
