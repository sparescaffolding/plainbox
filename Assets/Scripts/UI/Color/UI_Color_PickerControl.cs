using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class UI_Color_PickerControl : MonoBehaviour
{
    public float current_hue;
    public float current_saturation;
    public float current_value;

    public RawImage hue;
    public RawImage saturation_value;

    public Slider hue_slider;

    private Texture2D hue_texture;
    private Texture2D saturation_texture;

    public UI_Manager ui_manager;

    private void Start()
    {
        //initialize
        ui_manager = FindFirstObjectByType<UI_Manager>();
        
        CreateHueImage();
        CreateSaturationImage();
    }

    private void CreateHueImage()
    {
        //create texture
        hue_texture = new Texture2D(1, 16);
        hue_texture.wrapMode = TextureWrapMode.Clamp;
        hue_texture.name = "hue";
        
        for(int i = 0; i < hue_texture.height; i++)
        {
            hue_texture.SetPixel(0, i, Color.HSVToRGB((float)i / hue_texture.height, 1, 1f));
        }
        //apply
        hue_texture.Apply();
        current_hue = 0;
        hue.texture =  hue_texture;
    }

    private void CreateSaturationImage()
    {
        //setup saturation
        saturation_texture = new Texture2D(16, 16);
        saturation_texture.wrapMode = TextureWrapMode.Clamp;
        saturation_texture.name = "saturation";

        for (int y = 0; y < saturation_texture.height; y++)
        {
            for (int x = 0; x < saturation_texture.width; x++)
            {
                saturation_texture.SetPixel(x, y, Color.HSVToRGB(current_hue, (float)x / saturation_texture.width, (float)y / saturation_texture.height));
            }
        }
        
        //apply saturation image
        saturation_texture.Apply();
        current_saturation = 0;
        current_value = 0;

        saturation_value.texture = saturation_texture;
    }

    private void UpdateColor()
    {
        //initialize current selected color
        Color current = Color.HSVToRGB(current_hue, current_saturation, current_value);
        ui_manager.ui_manipulatemenu.manipulator.selected_object.GetComponent<MeshRenderer>().material.color = current;
    }

    public void UpdateSaturation()
    {
        current_hue = hue_slider.value;

        for (int y = 0; y < saturation_texture.height; y++)
        {
            for (int x = 0; x < saturation_texture.width; x++)
            {
                saturation_texture.SetPixel(x, y, Color.HSVToRGB(current_hue, (float)x / saturation_texture.width, (float)y / saturation_texture.height));
            }
        }
        
        saturation_texture.Apply();
        UpdateColor();
    }
    
    public void SetSaturation(float S, float V)
    {
        current_saturation = S; //saturation
        current_value = V;      //value
        
        UpdateColor();
    }
}
