using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UI_Color_SaturationControl : MonoBehaviour, IDragHandler, IPointerClickHandler
{
    public Image picker;
    public RawImage saturation_image;
    public UI_Color_PickerControl c;
    private RectTransform rect_t, picker_t;

    private void Awake()
    {
        //init
        c = FindObjectOfType<UI_Color_PickerControl>();
        rect_t = GetComponent<RectTransform>();
        picker_t = picker.GetComponent<RectTransform>();
    }

    private void Start()
    {
        //change position of transform
        picker_t.localPosition = new Vector2(-(rect_t.rect.width * 0.5f), (rect_t.rect.height * 0.5f));
    }

    void UpdateColor(PointerEventData eventData)
    {
        Vector2 localPos;
        // Change eventData.pressEventCamera to null for Screen Space - Overlay canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect_t, eventData.position, null, out localPos);

        float width = rect_t.rect.width;
        float height = rect_t.rect.height;

        float xClamp = width / 2;
        float yClamp = height / 2;
        localPos.x = Mathf.Clamp(localPos.x, -xClamp, xClamp);
        localPos.y = Mathf.Clamp(localPos.y, -yClamp, yClamp);

        picker_t.localPosition = localPos;

        float xnorm = (localPos.x + xClamp) / width;
        float ynorm = (localPos.y + yClamp) / height;

        c.SetSaturation(xnorm, ynorm);
    }

    public void Sync(float saturation, float value)
    {
        //get size
        float width = rect_t.rect.width;
        float height = rect_t.rect.height;
        
        //calculate position
        float x = (saturation * width) - (width * 0.5f);
        float y = (value * height) - (height * 0.5f);
        
        //set position
        picker_t.localPosition = new Vector2(x, y);
    }
    
    public void OnDrag(PointerEventData eventData) { UpdateColor(eventData); }
    public void OnPointerClick(PointerEventData eventData) { UpdateColor(eventData); }
}
