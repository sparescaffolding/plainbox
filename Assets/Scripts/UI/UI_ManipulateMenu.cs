using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ManipulateMenu : MonoBehaviour
{
    [Header("available properties")]
    public GameObject position;
    public GameObject rotation;
    public GameObject scale;
    public GameObject color;
    [Space]
    [Header("name field")]
    public TMP_InputField object_name;
    [Space]
    [Header("position fields")]
    public TMP_InputField x_position;
    public TMP_InputField y_position;
    public TMP_InputField z_position;
    [Space]
    [Header("rotation fields")]
    public TMP_InputField x_rotation;
    public TMP_InputField y_rotation;
    public TMP_InputField z_rotation;
    [Space]
    [Header("scale fields")]
    public TMP_InputField x_scale;
    public TMP_InputField y_scale;
    public TMP_InputField z_scale;
    [Space]
    public Tools_Manipulator manipulator;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        manipulator = FindFirstObjectByType<Tools_Manipulator>();
    }

    public void Load()
    {
        //disable all first
        this.position.SetActive(false);
        this.rotation.SetActive(false);
        this.scale.SetActive(false);
        this.color.SetActive(false);
        
        
        //if position modification is allowed
        if (manipulator.object_properties.position)
        {
            //enable field
            this.position.SetActive(true);
        }
        //if rotation modification is allowed
        if (manipulator.object_properties.rotation)
        {
            //enable field
            this.rotation.SetActive(true);
        }
        //if scale modification is allowed
        if (manipulator.object_properties.scale)
        {
            //enable field
            this.scale.SetActive(true);
        }
        //if color modification is allowed
        if (manipulator.object_properties.color)
        {
            //enable field
            color.SetActive(true);
        }
        
        //pos rot and scale
        Vector3 position = manipulator.selected_object.gameObject.transform.position;
        Vector3 rotation = manipulator.selected_object.gameObject.transform.eulerAngles;
        Vector3 scale = manipulator.selected_object.gameObject.transform.localScale;
        
        //get selected object name, append (Clone) from instantiation and set
        object_name.text = manipulator.selected_object.name.Replace("(Clone)", "").Trim();
        //position field initializing
        x_position.text = position.x.ToString();
        y_position.text = position.y.ToString();
        z_position.text = position.z.ToString();
        //scale field initializing
        x_rotation.text = rotation.x.ToString();
        y_rotation.text = rotation.y.ToString();
        z_rotation.text = rotation.z.ToString();
        //scale field initializing
        x_scale.text = scale.x.ToString();
        y_scale.text = scale.y.ToString();
        z_scale.text = scale.z.ToString();
        
        
        //apply new name
        object_name.onEndEdit.AddListener((value) => { manipulator.selected_object.name = value; });
        
        //position field applying
        //x
        x_position.onEndEdit.AddListener(value => { Vector3 position = manipulator.selected_object.transform.position; position.x = float.Parse(value); manipulator.selected_object.transform.position = position; });
        //y
        y_position.onEndEdit.AddListener(value => { Vector3 position = manipulator.selected_object.transform.position; position.y = float.Parse(value); manipulator.selected_object.transform.position = position; });
        //z
        z_position.onEndEdit.AddListener(value => { Vector3 position = manipulator.selected_object.transform.position; position.z = float.Parse(value); manipulator.selected_object.transform.position = position; });
        
        //rotation field applying
        //x
        x_rotation.onEndEdit.AddListener(value => { Vector3 rotation = manipulator.selected_object.transform.eulerAngles; rotation.x = float.Parse(value); manipulator.selected_object.transform.eulerAngles = rotation; });
        //y
        y_rotation.onEndEdit.AddListener(value => { Vector3 rotation = manipulator.selected_object.transform.eulerAngles; rotation.y = float.Parse(value); manipulator.selected_object.transform.eulerAngles = rotation; });
        //z
        z_rotation.onEndEdit.AddListener(value => { Vector3 rotation = manipulator.selected_object.transform.eulerAngles; rotation.z = float.Parse(value); manipulator.selected_object.transform.eulerAngles = rotation; });
        
        //scale field applying
        //x
        x_scale.onEndEdit.AddListener(value => { Vector3 scale = manipulator.selected_object.transform.localScale; scale.x = float.Parse(value); manipulator.selected_object.transform.localScale = scale; });
        //y
        y_scale.onEndEdit.AddListener(value => { Vector3 scale = manipulator.selected_object.transform.localScale; scale.y = float.Parse(value); manipulator.selected_object.transform.localScale = scale; });
        //z
        z_scale.onEndEdit.AddListener(value => { Vector3 scale = manipulator.selected_object.transform.localScale; scale.z = float.Parse(value); manipulator.selected_object.transform.localScale = scale; });
   }
}
