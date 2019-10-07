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
        growrate = 0.2;
        
        // Tiles
        tiles = new Dictionary<int, Tile>();

        Tile temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Trees/FirTree1_1.asset", typeof(Tile)) as Tile;
        tiles.Add(0, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Trees/FirTree1_2.asset", typeof(Tile)) as Tile;
        tiles.Add(1, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Trees/FirTree1_3.asset", typeof(Tile)) as Tile;
        tiles.Add(2, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Trees/FirTree1_4.asset", typeof(Tile)) as Tile;
        tiles.Add(3, temp);
    }
}