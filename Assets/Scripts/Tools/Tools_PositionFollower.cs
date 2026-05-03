using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools_PositionFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    
    void Update()
    {
        //follow and apply offset
        transform.position = target.position + offset;
    }
}
