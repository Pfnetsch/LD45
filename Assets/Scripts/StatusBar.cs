using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;
    public Image sliderFill;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StatusBarOnValueChanged(float value)
    {
        sliderFill.color = Color.Lerp(Color.red, Color.green, value);
    }
}
