using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grass : Vegetation
{
    public Grass()
    {
        name = "Grass";

        // mid/high
        waterReq = 0.5;
        
        // low
        waterMod = 0.2;
        
        // none
        flammability = 0.0;
        
        // mid
        infestability = 0.5;
        
        // low
        co2Usage = 0.2;
        
        // high
        growrate = 0.7;

        // high
        seedOrSaplingDropChance = 0.5;

        // low but not needed
        durability = 0.4;

        if (!tiles.ContainsKey(this.GetType().Name))
        {
            tiles[this.GetType().Name] = new Dictionary<int, Tile>();

            Tile temp = Resources.Load<Tile>("Tiles/Grass/Grass1_1");
            tiles[this.GetType().Name].Add(0, temp);

            temp = Resources.Load<Tile>("Tiles/Grass/Grass1_2");
            tiles[this.GetType().Name].Add(1, temp);

            temp = Resources.Load<Tile>("Tiles/Grass/Grass1_3");
            tiles[this.GetType().Name].Add(2, temp);

            temp = Resources.Load<Tile>("Tiles/Grass/Grass1_4");
            tiles[this.GetType().Name].Add(3, temp);
        }
    }
}