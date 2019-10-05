using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FirTree: Vegetation
{
    public FirTree()
    {
        this.name = "FirTree";
        this.tiles = new Dictionary<int, Tile>();

        Tile temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Trees/FirTree1_1.asset", typeof(Tile)) as Tile;
        this.tiles.Add(0, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Trees/FirTree1_2.asset", typeof(Tile)) as Tile;
        this.tiles.Add(1, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Trees/FirTree1_3.asset", typeof(Tile)) as Tile;
        this.tiles.Add(2, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Trees/FirTree1_4.asset", typeof(Tile)) as Tile;
        this.tiles.Add(3, temp);
    }
}