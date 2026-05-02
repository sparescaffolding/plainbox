using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PickupDistance : MonoBehaviour
{
    public float value;
    public float distance_minimum;
    public float distance_maximum;

    private void Update()
    {
        //scroll wheel value
        float scroll = Input.mouseScrollDelta.y;
        //update current value from scroll
        value += scroll;
        ///clamp to distance minimum and maximum
        value = Mathf.Clamp(value, distance_minimum, distance_maximum);
        //apply
        transform.localPosition = new Vector3(0, 0, value);;
    }
}
