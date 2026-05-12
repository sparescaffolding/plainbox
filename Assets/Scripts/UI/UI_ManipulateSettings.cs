using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ManipulateSettings : MonoBehaviour
{
    public Tools_Manipulator manipulator;
    public TextMeshProUGUI desc;

    //find as soon as menu is opened
    private void OnEnable()
    {
        if (manipulator == null)
        {
            manipulator = FindObjectOfType<Tools_Manipulator>();
        }
    }

    private void Update()
    {
        if (manipulator.constraint_state == Constraint.None)
        {
            desc.text = "None:\nNo constraints are selected!";
        }
        else if (manipulator.constraint_state == Constraint.Weld)
        {
            desc.text = "To attach an object to another one:\n\n- First select the object you want to weld.\n- Then select the other object which you want to weld the first one to.";
        }
        else if (manipulator.constraint_state == Constraint.Rope)
        {
            desc.text = "To create a simple rope constraint between two objects:\n\n- First select the object you want the rope to start from.\n- Then select the other object that you want the rope to end the connection at.";
        }
    }

    public void ConstraintStateChanged(int state)
    {
        manipulator.constraint_state = (Constraint)state;
    }
}
