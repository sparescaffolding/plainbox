using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CameraHolder : MonoBehaviour
{
    public Transform camera_point;

    private void Update()
    {
        //move the camera setup to the same position as the camera_point transform
        transform.position = camera_point.position;
    }
}
