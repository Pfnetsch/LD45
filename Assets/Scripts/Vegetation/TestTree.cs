
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestTree: Vegetation
{
    public TestTree()
    {
        this.name = "TestTree";
        this.tiles = new Dictionary<int, Tile>();

        Tile temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Tree.asset", typeof(Tile)) as Tile;
        this.tiles.Add(0, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Water.asset", typeof(Tile)) as Tile;
        this.tiles.Add(1, temp);
    }
}