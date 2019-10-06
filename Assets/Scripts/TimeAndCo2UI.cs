using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeAndCo2UI : MonoBehaviour
{
    public HexMap hexMap;
    public Transform co2GaugePointer;
    public Transform dateText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        co2GaugePointer.rotation = new Quaternion(0, 0, -(float)(hexMap.getCO2Level() * 2 - 1), 1);
        dateText.GetComponent<Text>().text = hexMap.getDate().ToString("MMM yyyy");
    }
}
