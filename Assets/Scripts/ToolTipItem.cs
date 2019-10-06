using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipItem : MonoBehaviour
{
    public SVGImage toolTipImage;
    public Text toolTipText;
    public Slider slider;
    public Image sliderFill;
    public bool revertColors;

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
        sliderFill.color = Color.Lerp(new Color(0xd4/255.0f, 0x57 / 255.0f, 0x3b / 255.0f) , new Color(0x53 / 255.0f, 0xbf / 255.0f, 0x5c / 255.0f), revertColors ? 1-value : value);
    }
}
