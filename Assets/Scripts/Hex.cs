using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hex
{
    public enum TERRAIN_TYPE { DEFAULT, PLAINS, MARSH, DESERT, OCEAN }
    public TERRAIN_TYPE terrainType;
    
    public readonly HexMap hexMap;

    private Vegetation vegetation;
    private int level = 0;
    
    
    
    public Hex(HexMap hexMap, TERRAIN_TYPE terrainType = TERRAIN_TYPE.DEFAULT)
    {
        this.hexMap = hexMap;
        this.terrainType = terrainType;
        this.vegetation = new Vegetation();
    }

    public Boolean hasVegetation()
    {
        return this.vegetation != null;
    }
    
    public Vegetation getVegetation()
    {
        return this.vegetation;
    }
    
    public void setVegetation(Vegetation vegetation)
    {
        this.vegetation = vegetation;
    }

    public void removeVegetation()
    {
        this.level = 0;
        this.vegetation = null;
    }

    public void upgrade()
    {
        if (this.level < this.vegetation.getMaxLevel())
        {
            this.level += 1;
        }
    }

    public Tile getCurrentTile()
    {
        return this.vegetation.getTileForLevel(level);
    }

    public Boolean isEmpty()
    {
        return vegetation.getName().Equals("empty");
    }
}
