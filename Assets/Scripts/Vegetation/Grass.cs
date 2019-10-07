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
        
        tiles = new Dictionary<int, Tile>();

        Tile temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Grass/Grass1_1.asset", typeof(Tile)) as Tile;
        tiles.Add(0, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Grass/Grass1_2.asset", typeof(Tile)) as Tile;
        tiles.Add(1, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Grass/Grass1_3.asset", typeof(Tile)) as Tile;
        tiles.Add(2, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Grass/Grass1_4.asset", typeof(Tile)) as Tile;
        tiles.Add(3, temp);
    }
}