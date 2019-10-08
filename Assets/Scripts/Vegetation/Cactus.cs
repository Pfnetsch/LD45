using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cactus : Vegetation
{
    public Cactus()
    {
        name = "Cactus";

        // very low
        waterReq = 0.1;
        
        // high
        waterMod = 0.8;
        
        // low
        flammability = 0.3;
        
        // low
        infestability = 0.5;
        
        // no
        co2Usage = 0.0;
        
        // mid
        growrate = 0.5;

        // low
        seedOrSaplingDropChance = 0.7;

        // high
        durability = 0.8;

        // Tiles
        tiles = new Dictionary<int, Tile>();

        Tile temp = Resources.Load<Tile>("Tiles/Cactus/Cactus1_1");
        tiles.Add(0, temp);
        
        temp = Resources.Load<Tile>("Tiles/Cactus/Cactus1_2");
        tiles.Add(1, temp);
        
        temp = Resources.Load<Tile>("Tiles/Cactus/Cactus1_3");
        tiles.Add(2, temp);
        
        temp = Resources.Load<Tile>("Tiles/Cactus/Cactus1_4");
        tiles.Add(3, temp);
    }
}