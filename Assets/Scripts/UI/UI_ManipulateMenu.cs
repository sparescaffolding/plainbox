using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ManipulateMenu : MonoBehaviour
{
    [Header("name field")]
    public TMP_InputField object_name;
    [Space]
    [Header("scale fields")]
    public TMP_InputField x;
    public TMP_InputField y;
    public TMP_InputField z;
    
    public Tools_Manipulator manipulator;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        manipulator = FindFirstObjectByType<Tools_Manipulator>();
    }

    public void Load()
    {
        //get selected object name, append (Clone) from instantiation and set
        object_name.text = manipulator.selected_object.name.Replace("(Clone)", "").Trim();
        
        //apply new name
        object_name.onSubmit.AddListener((value) => { manipulator.selected_object.name = value; });
        
        //scale field applying
        x.onSubmit.AddListener(value => { manipulator.selected_object.transform.localScale.x = value; });
    }
}
