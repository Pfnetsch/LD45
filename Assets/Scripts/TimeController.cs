using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    private HexMap hexMap;
    
    // Start is called before the first frame update
    void Start()
    {
        hexMap = FindObjectOfType<HexMap>();
    }

    // Update is called once per frame
    void Update()
    {
        // advance time
        
        // calculate co2, water, etc and update ui
        
        // calculate chance for infestation, fire, tsunami,  (new and spreading)
    }
}
