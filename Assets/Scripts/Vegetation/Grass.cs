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
        
        // none
        infestability = 0.0;
        
        // low
        co2Usage = 0.2;
        
        // high
        growrate = 0.8;

        // high
        seedOrSaplingDropChance = 0.75;

        tiles = new Dictionary<int, Tile>();
        
        Tile temp = Resources.Load<Tile>("Tiles/Trees/Grass1_1");
        tiles.Add(0, temp);
        
        temp = Resources.Load<Tile>("Tiles/Trees/Grass1_2");
        tiles.Add(1, temp);
        
        temp = Resources.Load<Tile>("Tiles/Trees/Grass1_3");
        tiles.Add(2, temp);
        
        temp = Resources.Load<Tile>("Tiles/Trees/Grass1_4");
        tiles.Add(3, temp);
    }
}