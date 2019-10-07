using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public int ticksPerCylce = 100;

    private HexMap hexMap;
    private int tickCount = 0;

    private bool fasterActive = false;

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

        if (Input.GetMouseButtonUp(0) && fasterActive)
        {
            ticksPerCylce *= 4;
            fasterActive = false;
        }
    }

    private void FixedUpdate()
    {
        tickCount++;

        if (tickCount % (ticksPerCylce/16) == 0)
        {
            hexMap.doWaterTick();
            
            if (tickCount % ticksPerCylce == 0)
            {
                hexMap.doFireTick();
                hexMap.doInfestationTick();
                hexMap.doCO2Tick();
                hexMap.doGrowTick();
                hexMap.doDeathTick();
                hexMap.updateMapVisuals();

                tickCount = 0;
            }
            
            hexMap.updateMapVisuals();
        }

        // map 20min to 40yr - 1051200 * 100 / default ticks
        // map 1min to 40yr  - 21024000 * 100 / default ticks
        hexMap.date = hexMap.date.AddSeconds(Time.fixedDeltaTime * 105120000 / ticksPerCylce);
    }

    public void ButtonFasterPointerDown()
    {
        ticksPerCylce /= 4;
        fasterActive = true;
    }
}
