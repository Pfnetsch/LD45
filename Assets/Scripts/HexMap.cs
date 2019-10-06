using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class HexMap : MonoBehaviour
{
    // adjust water generation
    private const double WATER_SPREAD = 1.4;

    public Tile defaultTile;
    public Tile desertTile;
    public Tile plainsTile;
    public Tile marshTile;
    public Tile oceanTile;


    public int numRows = 30;
    public int numColumns = 60;
    private int startRow = 0;
    private int startColumn = 0;

    // graphic tiles
    public Tilemap overlayTilemap;
    public Tilemap foregroundTilemap;
    public Tilemap backgroundTilemap;

    // hexdata
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
                Hex h = new Hex(this, column, row, Hex.TERRAIN_TYPE.DESERT);

                if (column == 1 && row == 1)
                {
                    h.terrainType = Hex.TERRAIN_TYPE.OCEAN;
                }
                
                hexes[column, row] = h;
            }
        }

        updateMapVisuals();
    }

    public Hex getHexAt(int x, int y)
    {
        if (hexes == null)
        {
            Debug.LogError("Hexes array not yet instantiated.");
            return null;
        }

        try
        {
            return hexes[x, y];
        }
        catch
        {
            //Debug.LogError("GetHexAt: " + x + "," + y);
            return null;
        }
    }

    void updateMapVisuals()
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

                backgroundTilemap.SetTile(new Vector3Int(row + startRow, column + startColumn, 0), tile);
                backgroundTilemap.SetTileFlags(new Vector3Int(row + startRow, column + startColumn, 0), TileFlags.None);
                backgroundTilemap.SetColor(new Vector3Int(row + startRow, column + startColumn, 0), new Color(0.0f,0.0f,(float)hexes[column, row].getWaterLevel()));


                // tile not empty => render foreground
                if (!hexes[column, row].isEmpty())
                {
                    foregroundTilemap.SetTile(new Vector3Int(row + startRow, column + startColumn, 0),
                        hexes[column, row].getCurrentTile());
                }
            }
        }
    }

    public void upgradeTile(Vector3Int position)
    {
        if (position.x >= this.startColumn && position.y >= this.startRow &&
            position.x < this.startColumn + this.numColumns &&
            position.y < this.startRow + this.numRows)
        {
            this.hexes[position.y, position.x].upgrade();
            this.updateMapVisuals();
        }
    }

    public void plantVegetation(Vector3Int position, Vegetation vegetation)
    {
        if (position.x >= this.startColumn && position.y >= this.startRow && position.x < this.startColumn + this.numColumns &&
               position.y < this.startRow + this.numRows)
        {
            this.hexes[position.y, position.x].setVegetation(vegetation);
            this.updateMap();
        }
    }

    public void doWaterTick()
    {
        double[,] newWater = new double[numColumns, numRows];
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex[] neighbours = hexes[column, row].getNeighbours();
                double newWaterLevel = 0.0;

                foreach (Hex neighbour in neighbours)
                {
                    if (neighbour.getWaterLevel() > hexes[column, row].getWaterLevel())
                    {
                        newWaterLevel += neighbour.getWaterLevel();
                    }
                }

                // TODO: adjust water spread formula
                newWater[column, row] = newWaterLevel / 6.0 * WATER_SPREAD;
            }
        }

        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                hexes[column, row].setWaterLevel(newWater[column, row]);
            }
        }
        
        updateMapVisuals();
    }
}
