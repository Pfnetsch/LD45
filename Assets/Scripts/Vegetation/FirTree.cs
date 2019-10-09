using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FirTree: Vegetation
{
    public FirTree()
    {
        name = "FirTree";

        // high
        waterReq = 0.6;
        
        // high
        waterMod = 0.8;
        
        // high
        flammability = 0.8;
        
        // mid
        infestability = 0.5;
        
        // high
        co2Usage = 0.8;
        
        // low
        growrate = 0.4;

        // low
        seedOrSaplingDropChance = 0.9;

        // high
        durability = 1;

        // Tiles
        if (!tiles.ContainsKey(this.GetType().Name))
        {
            tiles[this.GetType().Name] = new Dictionary<int, Tile>();

            Tile temp = Resources.Load<Tile>("Tiles/Trees/FirTree1_1");
            tiles[this.GetType().Name].Add(0, temp);

            temp = Resources.Load<Tile>("Tiles/Trees/FirTree1_2");
            tiles[this.GetType().Name].Add(1, temp);

            temp = Resources.Load<Tile>("Tiles/Trees/FirTree1_3");
            tiles[this.GetType().Name].Add(2, temp);

            temp = Resources.Load<Tile>("Tiles/Trees/FirTree1_4");
            tiles[this.GetType().Name].Add(3, temp);
        }
    }
}