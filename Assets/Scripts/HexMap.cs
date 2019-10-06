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
    private const double WATER_SPREAD = 1.5;
    public const double WATER_LEVEL_HIGH = 0.4;
    public const double WATER_LEVEL_MID = 0.2;

    public Tile defaultTile;
    //public Tile desertTile;
    //public Tile plainsTile;
    //public Tile marshTile;
    //public Tile oceanTile;


    public int numRows = 50;
    public int numColumns = 50;
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

        Maps startMap = new Maps(); // make a new maps object
        startMap.CreateNewStartMap();
        

        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex h;
                switch(startMap.GetTileAtPosition(row, column))
                {
                    case 0:
                        h = new Hex(this, column, row, Hex.TERRAIN_TYPE.DESERT);
                        break;
                    case 1:
                        h = new Hex(this, column, row, Hex.TERRAIN_TYPE.PLAINS);
                        break;
                    case 2:
                        h = new Hex(this, column, row, Hex.TERRAIN_TYPE.OCEAN);
                        break;
                    default:
                        h = new Hex(this, column, row, Hex.TERRAIN_TYPE.DEFAULT);
                        break;
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
                Hex currentHex = hexes[column, row];
                Tile tile = defaultTile;
                Color tileColor;

                if (currentHex.terrainType == Hex.TERRAIN_TYPE.OCEAN)
                {
                    //6BDEFF
                    tileColor = new Color(0x6b/255.0f, 0xde/255.0f, 0xff/255.0f);
                }
                else if (currentHex.terrainType == Hex.TERRAIN_TYPE.DESERT)
                {
                    double waterLevel = currentHex.getWaterLevel();
                    if (waterLevel > WATER_LEVEL_HIGH)
                    {
                        //99701C
                        tileColor = new Color(0x99/255.0f, 0x70/255.0f, 0x1C/255.0f);
                    }
                    else if (waterLevel > WATER_LEVEL_MID)
                    {
                        //D3A450
                        tileColor = new Color(0xD3/255.0f, 0xA4/255.0f, 0x50/255.0f);
                    }
                    else
                    {
                        //FFDA83
                        tileColor = new Color(0xFF/255.0f, 0xDA/255.0f, 0x83/255.0f);
                    }
                }
                else
                {
                    tileColor = Color.magenta;
                }

                backgroundTilemap.SetTile(new Vector3Int(row + startRow, column + startColumn, 0), tile);
                backgroundTilemap.SetTileFlags(new Vector3Int(row + startRow, column + startColumn, 0), TileFlags.None);
                backgroundTilemap.SetColor(new Vector3Int(row + startRow, column + startColumn, 0), tileColor);


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
            this.updateMapVisuals();
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

                if (newWater[column, row] > 1.0)
                {
                    newWater[column, row] = 1.0;
                }
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
