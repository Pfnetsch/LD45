using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateMap : MonoBehaviour
{
    public Tile snowTile;
    public Tile desertTile;
    public Tile treeTile;
    public Tile waterTile;

    public Tilemap tileMap;
    
    // Start is called before the first frame update
    void Start()
    {
        int xCount = tileMap.size.x;
        int yCount = tileMap.size.y;

        for (int x = 0; x < xCount; x++)
        {
            for (int y = 0; y < yCount; y++)
            {
                Vector3Int pos = new Vector3Int(x, y, 0);

                tileMap.SetTile(pos, treeTile);
            }
        }


        //TileMap.SetTile()

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
