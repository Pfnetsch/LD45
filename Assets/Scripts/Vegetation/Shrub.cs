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
        flammability = 0.8;
        
        // none
        infestability = 0.0;
        
        // mid
        co2Usage = 0.5;
        
        // mid
        growrate = 0.5;
        
        tiles = new Dictionary<int, Tile>();

        Tile temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Shrub/Shrub1_1.asset", typeof(Tile)) as Tile;
        tiles.Add(0, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Shrub/Shrub1_2.asset", typeof(Tile)) as Tile;
        tiles.Add(1, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Shrub/Shrub1_3.asset", typeof(Tile)) as Tile;
        tiles.Add(2, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Shrub/Shrub1_4.asset", typeof(Tile)) as Tile;
        tiles.Add(3, temp);
    }
}