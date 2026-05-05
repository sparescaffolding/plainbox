using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Manipulatable : MonoBehaviour
{
    public bool position;
    public bool rotation;
    public bool scale;
    public bool color;
    public bool mass;
    public bool trigger;
    public bool damage;
    public bool health;
    public Object_DamageTrigger damage_trigger;
    public Object_HealthTrigger health_trigger;

    private void Start()
    {
        //init
        damage_trigger =  gameObject.AddComponent<Object_DamageTrigger>();
        health_trigger =  gameObject.AddComponent<Object_HealthTrigger>();
    }
}
