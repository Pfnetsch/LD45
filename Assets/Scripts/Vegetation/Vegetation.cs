using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vegetation
{
    protected Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();
    protected String name = "empty";
    
    // water modifier (<1.0f for reduction, >1.0f for increase)
    protected double waterMod = 1.0;
    
    // properties [0.0-1.0]?
    protected double waterReq = 0.2;

    protected double flammability = 0.0;

    protected double infestability = 0.0;

    protected double co2Usage = 0.0;

    protected double growrate = 0.0;
    
    public int getMaxLevel()
    {
        return this.tiles.Count - 1;
    }

    public String getName()
    {
        return name;
    }

    public Tile getTileForLevel(int level)
    {
        if (level <= getMaxLevel())
        {
            return tiles[level];
        }

        Debug.Log("Bad level");
        return new Tile();
    }
}
