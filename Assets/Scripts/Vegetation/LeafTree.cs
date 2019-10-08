
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
        growrate = 0.4;

        // low
        seedOrSaplingDropChance = 0.9;

        // high
        durability = 0.8;

        if (!tiles.ContainsKey(this.GetType().Name))
        {
            tiles[this.GetType().Name] = new Dictionary<int, Tile>();

            Tile temp = Resources.Load<Tile>("Tiles/Trees/LeafTree1_1");
            tiles[this.GetType().Name].Add(0, temp);

            temp = Resources.Load<Tile>("Tiles/Trees/LeafTree1_2");
            tiles[this.GetType().Name].Add(1, temp);

            temp = Resources.Load<Tile>("Tiles/Trees/LeafTree1_3");
            tiles[this.GetType().Name].Add(2, temp);

            temp = Resources.Load<Tile>("Tiles/Trees/LeafTree1_4");
            tiles[this.GetType().Name].Add(3, temp);
        }
    }
}