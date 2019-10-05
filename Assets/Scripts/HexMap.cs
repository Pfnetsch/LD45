using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexMap : MonoBehaviour
{
    public Tile defaultTile;
    public Tile desertTile;
    public Tile plainsTile;
    public Tile marshTile;
    public Tile oceanTile;

    
    public int numRows = 30;
    public int numColumns = 60;
    private int startRow = 0;
    private int startColumn = 0;

    public Tilemap tilemap;
    private Hex[,] hexes;


    // Start is called before the first frame update
    void Start()
    {
        generateMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    virtual public void generateMap()
    {
        // Generate a map filled with ocean

        hexes = new Hex[numColumns, numRows];

        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h = new Hex(this);

                hexes[column, row] = h;
            }
        }

        updateMap();
    }

    void updateMap()
    {
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Tile tile;
                switch (hexes[column, row].terrainType)
                {
                    case Hex.TERRAIN_TYPE.DESERT:
                        tile = desertTile;
                        break;
                    case Hex.TERRAIN_TYPE.MARSH:
                        tile = marshTile;
                        break;
                    case Hex.TERRAIN_TYPE.OCEAN:
                        tile = oceanTile;
                        break;
                    case Hex.TERRAIN_TYPE.PLAINS:
                        tile = plainsTile;
                        break;
                    default:
                        tile = defaultTile;
                        break;
                }

                tilemap.SetTile(new Vector3Int(row + startRow, column + startColumn, 0), tile);
            }
        }
    }
}
