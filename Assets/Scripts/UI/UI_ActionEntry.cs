using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ActionEntry : MonoBehaviour
{
    public float time = 2f;
    public TextMeshProUGUI text;
    
    private Graphic[] visuals;      //all panels, images, etc.

    private void Start()
    {
        //find all
        visuals = GetComponentsInChildren<Graphic>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        
        StartCoroutine(wait(time));
    }

    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(fade(time));
        yield return null;
    }
    
    IEnumerator fade(float time)
    {
        float timer = 0f;

        while (timer < time)
        {
            timer += Time.deltaTime;
            //calculate fade out progress
            float opacity = 1f  - (timer / time);
            //apply
            foreach (Graphic v in visuals)
            {
                Color c = v.color;
                c.a = opacity;
                v.color = c;
            }

            //wait frame
            yield return null;
        }
        
        //destroy to keep list clean
        Destroy(gameObject);
    }
}
