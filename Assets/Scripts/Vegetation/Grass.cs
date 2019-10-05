using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Grass: Vegetation
{
    public Grass()
    {
        this.name = "Grass";
        this.tiles = new Dictionary<int, Tile>();

        Tile temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Grass/Grass1_1.asset", typeof(Tile)) as Tile;
        this.tiles.Add(0, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Grass/Grass1_2.asset", typeof(Tile)) as Tile;
        this.tiles.Add(1, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Grass/Grass1_3.asset", typeof(Tile)) as Tile;
        this.tiles.Add(2, temp);
        
        temp = AssetDatabase.LoadAssetAtPath("Assets/Tiles/Grass/Grass1_4.asset", typeof(Tile)) as Tile;
        this.tiles.Add(3, temp);
    }
}