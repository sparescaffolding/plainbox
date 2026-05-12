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
        //only add component if true
        if (damage)
        {
            damage_trigger = gameObject.AddComponent<Object_DamageTrigger>();
        }
        if (health)
        {
            health_trigger =  gameObject.AddComponent<Object_HealthTrigger>();
        }
    }

    private void Update()
    {
        //add back when true
        if(damage) { if(!damage_trigger) { damage_trigger = gameObject.AddComponent<Object_DamageTrigger>(); } else { return; } }
        if(health) { if(!health_trigger) { health_trigger = gameObject.AddComponent<Object_HealthTrigger>(); } else { return; } }
        
        //remove when false
        if(!damage) { if(damage_trigger) { Destroy(damage_trigger); } else { return; } }
        if(!health) { if(health_trigger) { Destroy(health_trigger); } else { return; } }
    }
}
