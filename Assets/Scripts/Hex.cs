using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Hex
{
    public enum TERRAIN_TYPE { DEFAULT, PLAINS, MARSH, DESERT, OCEAN }
    public TERRAIN_TYPE terrainType;
    
    public readonly HexMap hexMap;

    private int q;
    private int r;
    private Vegetation vegetation;
    private int level = 0;
    private double waterLevel = 0.0;
    private Boolean burning = false;
    private Boolean infested = false;

    public Hex(HexMap hexMap, int q, int r, TERRAIN_TYPE terrainType = TERRAIN_TYPE.DEFAULT)
    {
        this.hexMap = hexMap;
        this.q = q;
        this.r = r;
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

    public Boolean isWaterSource()
    {
        if (this.terrainType == TERRAIN_TYPE.OCEAN)
        {
            return true;
        }

        return false;
    }

    public double getWaterLevel()
    {
        // water sources allways full
        if (this.isWaterSource())
        {
            return 1.0;
        }
        
        return this.waterLevel;
    }
    
    public double getMaxWaterLevel()
    {
        // TODO: return real max level which is modified by vegetation
        return 1.0;
    }

    public void setWaterLevel(double waterLevel)
    {
        this.waterLevel = waterLevel;
    }

    public void setBurning(bool burning)
    {
        this.burning = burning;
    }
    
    public Boolean isBurning()
    {
        return burning;
    }

    public Boolean isInfested()
    {
        return infested;
    }

    public Vector3Int getPosition()
    {
        return new Vector3Int(r, q, 0);
    }


    Hex[] neighbours;

    public Hex[] getNeighbours()
    {
        if(this.neighbours != null)
            return this.neighbours;
        
        List<Hex> neighbours = new List<Hex>();
        
        if (q % 2 == 0)
        {
            // even row
            neighbours.Add( hexMap.getHexAt( q + -1,  r +  -1 ) );
            neighbours.Add( hexMap.getHexAt( q + +1,  r +  -1 ) );
        }
        else
        {
            // odd row
            neighbours.Add( hexMap.getHexAt( q + +1,  r +  +1 ) );
            neighbours.Add( hexMap.getHexAt( q + -1,  r +  +1 ) );
        }
        
        neighbours.Add( hexMap.getHexAt( q + +1,  r +  0 ) );
        neighbours.Add( hexMap.getHexAt( q + -1,  r +  0 ) );
        neighbours.Add( hexMap.getHexAt( q +  0,  r + +1 ) );
        neighbours.Add( hexMap.getHexAt( q +  0,  r + -1 ) );

        List<Hex> neighbours2 = new List<Hex>();

        foreach(Hex h in neighbours)
        {
            if(h != null)
            {
                neighbours2.Add(h);
            }
        }

        this.neighbours = neighbours2.ToArray();

        return this.neighbours;
    }
}
