using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManipulateMenu : MonoBehaviour
{
    [Header("available properties")]
    public GameObject position;
    public GameObject rotation;
    public GameObject scale;
    public GameObject color;
    public GameObject mass;
    public GameObject health_property;
    public GameObject damage_property;
    public GameObject toggle_trigger;
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
    [Space] [Header("trigger stuff")]
    public Toggle object_trigger_gravity;
    public TMP_InputField damage;
    public TMP_InputField health;
    [Space]
    public TMP_InputField weight;
    [Space]
    public Tools_Manipulator manipulator;
    
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void OnEnable()
    {
        manipulator = FindFirstObjectByType<Tools_Manipulator>();
        object_trigger_gravity.onValueChanged.AddListener(ToggleTriggerGravity);
    }

    public void Load()
    {
        //disable all first
        this.position.SetActive(false);
        this.rotation.SetActive(false);
        this.scale.SetActive(false);
        color.SetActive(false);
        mass.SetActive(false);
        
        rb = manipulator.selected_object.gameObject.GetComponent<Rigidbody>();
        
        
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
        //if mass modification is allowed
        if (manipulator.object_properties.mass)
        {
            //enable field
            mass.SetActive(true);
        }
        //if trigger modification is allowed
        if (manipulator.object_properties.trigger)
        {
            //enable field
            toggle_trigger.SetActive(true);
        }
        //if health modification is allowed
        if (manipulator.object_properties.health)
        {
            //enable field
            health_property.SetActive(true);
        }
        //if damage modification is allowed
        if (manipulator.object_properties.damage)
        {
            //enable field
            damage_property.SetActive(true);
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
        //weight field initializing
        weight.text = rb.mass.ToString();
        
        
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
        
        //trigger stuff
        //trigger
        //look at void ToggleTriggerGravity below 
        //damage
        damage.onEndEdit.AddListener((value) => {if(int.TryParse(value, out int result)) {manipulator.object_properties.damage_trigger.damage = result; }});
        //health
        health.onEndEdit.AddListener((value) => {if(int.TryParse(value, out int result)) {manipulator.object_properties.health_trigger.amount = result; }});
        
        //apply new weight
        weight.onEndEdit.AddListener(value => {if(float.TryParse(value, out float val)) { rb.mass = val; }});
   }

    void ToggleTriggerGravity(bool is_on)
    {
        if (is_on)
        {
            rb.useGravity = false;
            Collider[] colliders =manipulator.selected_object. GetComponentsInChildren<Collider>();

            //collider stuff
            foreach (Collider col in colliders)
            {
                col.isTrigger = true;
            }
        }
        else
        {
            rb.useGravity = true;
            Collider[] colliders = manipulator.selected_object.GetComponentsInChildren<Collider>();

            //collider stuff
            foreach (Collider col in colliders)
            {
                col.isTrigger = false;
            }
        }
    }
}
