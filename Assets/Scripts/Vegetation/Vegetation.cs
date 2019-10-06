using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vegetation
{
    protected Dictionary<int, Tile> tiles = new Dictionary<int, Tile>();
    protected String name = "empty";
    protected int maxLevel = 0;
    
    // water modifier (<1.0f for reduction, >1.0f for increase)
    protected float waterMod = 1.0f;
    
    // add more properties

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
        else
        {
            Debug.Log("Bad level");
            return new Tile();
        }
    }
}
