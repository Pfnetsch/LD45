
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LeafTree: Vegetation
{
    public LeafTree()
    {
        this.name = "LeafTree";
        this.tiles = new Dictionary<int, Tile>();

        Tile temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/LeafTree1_1.asset", typeof(Tile)) as Tile;
        this.tiles.Add(0, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/LeafTree1_2.asset", typeof(Tile)) as Tile;
        this.tiles.Add(1, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/LeafTree1_3.asset", typeof(Tile)) as Tile;
        this.tiles.Add(2, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/LeafTree1_4.asset", typeof(Tile)) as Tile;
        this.tiles.Add(3, temp);
    }
}