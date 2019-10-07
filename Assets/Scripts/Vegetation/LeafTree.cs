
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LeafTree: Vegetation
{
    public LeafTree()
    {
        name = "LeafTree";

        // very high
        waterReq = 0.7;
        
        // high
        waterMod = 0.8;
        
        // mid
        flammability = 0.5;
        
        // high
        infestability = 0.8;
        
        // very high
        co2Usage = 1.0;
        
        // low
        growrate = 0.2;

        // low
        seedOrSaplingDropChance = 0.2;

        // high
        durability = 0.8;

        tiles = new Dictionary<int, Tile>();
        
        Tile temp = Resources.Load<Tile>("Tiles/Trees/LeafTree1_1");
        tiles.Add(0, temp);
        
        temp = Resources.Load<Tile>("Tiles/Trees/LeafTree1_2");
        tiles.Add(1, temp);
        
        temp = Resources.Load<Tile>("Tiles/Trees/LeafTree1_3");
        tiles.Add(2, temp);
        
        temp = Resources.Load<Tile>("Tiles/Trees/LeafTree1_4");
        tiles.Add(3, temp);
    }
}