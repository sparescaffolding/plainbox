using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    //sensitivity axis and rotation stuff
    public float sensitivity_x;
    public float sensitivity_y;
    private float rotation_x;
    private float rotation_y;

    public Transform orientation;

    private void Start()
    {
        //lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //get mouse input axis
        float mouse_x = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity_x;
        float mouse_y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity_y;
        rotation_y += mouse_x;
        rotation_x -= mouse_y;
        //clamp vertical rotation to 90 degreees
        rotation_x = Mathf.Clamp(rotation_x, -90, 90);
        //apply rotations to the camera and orientation transform
        transform.rotation = Quaternion.Euler(rotation_x, rotation_y, 0);
        orientation.rotation = Quaternion.Euler(0, rotation_y, 0);
    }
}
