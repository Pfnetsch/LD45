using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Shrub: Vegetation
{
    public Shrub()
    {
        name = "Shrub";

        // low
        waterReq = 0.2;
        
        // mid
        waterMod = 0.3;
        
        // high
        flammability = 0.9;
        
        // none
        infestability = 0.0;
        
        // mid
        co2Usage = 0.5;
        
        // mid
        growrate = 0.7;

        // mid
        seedOrSaplingDropChance = 0.5;

        // low
        durability = 0.6;

        if (!tiles.ContainsKey(this.GetType().Name))
        {
            tiles[this.GetType().Name] = new Dictionary<int, Tile>();

            Tile temp = Resources.Load<Tile>("Tiles/Shrub/Shrub1_1");
            tiles[this.GetType().Name].Add(0, temp);

            temp = Resources.Load<Tile>("Tiles/Shrub/Shrub1_2");
            tiles[this.GetType().Name].Add(1, temp);

            temp = Resources.Load<Tile>("Tiles/Shrub/Shrub1_3");
            tiles[this.GetType().Name].Add(2, temp);

            temp = Resources.Load<Tile>("Tiles/Shrub/Shrub1_4");
            tiles[this.GetType().Name].Add(3, temp);
        }
    }
}