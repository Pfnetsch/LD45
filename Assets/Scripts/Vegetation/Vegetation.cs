using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Vegetation
{
    private static Dictionary<string, int> seedsOrSaplings = new Dictionary<string, int>();

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

    protected double seedOrSaplingDropChance = 0.0;

    protected double durability = 0.0;

    public int SeedsOrSaplings
    {
        get => seedsOrSaplings.ContainsKey(this.GetType().Name) ? seedsOrSaplings[this.GetType().Name] : default;
        set => seedsOrSaplings[this.GetType().Name] = value;
    }

    public static int getSeedsOrSaplings(Type veggie)
    {
        return seedsOrSaplings.ContainsKey(veggie.Name) ? seedsOrSaplings[veggie.Name] : default;
    }

    public static void setSeedsOrSaplings(Type veggie, int value)
    {
        seedsOrSaplings[veggie.Name] = value;
    }

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

    public double getWaterMod()
    {
        return waterMod;
    }

    public double getWaterRequirement()
    {
        return waterReq;
    }
    
    public double getFlammability()
    {
        return flammability;
    }
    
    public double getInfestability()
    {
        return infestability;
    }
    
    public double getCO2Usage()
    {
        return co2Usage;
    }
    
    public double getGrowrate()
    {
        return growrate;
    }

    public void harvestSeedOrSapling()
    {
        if (UnityEngine.Random.value <= seedOrSaplingDropChance)
        {
            // Success
            seedsOrSaplings[this.GetType().Name]++;
        }
    }

    public double getDurability()
    {
        return durability;
    }
}
