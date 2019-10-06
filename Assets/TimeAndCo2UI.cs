using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAndCo2UI : MonoBehaviour
{
    public HexMap hexMap;
    public Transform co2GaugePointer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        co2GaugePointer.rotation = new Quaternion(0, 0, -(float)(hexMap.getCO2Level() * 2 - 1), 1);
    }
}
