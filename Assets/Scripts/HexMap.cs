using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class HexMap : MonoBehaviour
{
    // adjust water generation
    public const double WATER_SPREAD = 0.7;
    public const double WATER_LEVEL_HIGH = 0.4;
    public const double WATER_LEVEL_MID = 0.2;

    public const double FIRE_SPREAD = 0.1;
    public const double INFESTATION_SPREAD = 0.1;
    public const double INFESTATION_MULT = 0.5;
    

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
    
    // states
    private Boolean lightning = false;
    
    // global co2
    private double co2 = 10000.0;
    private double co2change = 0.0;


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

    public Boolean canGrow(Vector3Int position, Vegetation vegetation)
    {
        if (position.x >= this.startColumn && position.y >= this.startRow &&
            position.x < this.startColumn + this.numColumns &&
            position.y < this.startRow + this.numRows)
        {
            return getHexAt(position.y, position.x).getWaterLevel() >= vegetation.getWaterRequirement();
        }

        return false;
    }
    
    public void plantVegetation(Vector3Int position, Vegetation vegetation)
    {
        if (position.x >= this.startColumn && position.y >= this.startRow && position.x < this.startColumn + this.numColumns &&
               position.y < this.startRow + this.numRows)
        {
            if (canGrow(position, vegetation))
            {
                this.hexes[position.y, position.x].setVegetation(vegetation);
                this.updateMapVisuals();
            }
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
                    if (neighbour.getWaterLevel() > hexes[column, row].getWaterLevel() && neighbour.getWaterLevel() > newWaterLevel)
                    {
                        newWaterLevel = neighbour.getWaterLevel();
                    }
                }

                // TODO: adjust water spread formula
                newWater[column, row] = newWaterLevel * WATER_SPREAD;

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

    public void doFireTick()
    {
        // new fire?
        if (lightning)
        {
            List<Vector3Int> vegetationHexes = (from Hex hex in hexes where hex.hasVegetation() select hex.getPosition()).ToList();

            // select random tile and try to spread lightning
            int index = Random.Range(0, vegetationHexes.Count);

            // set on fire?
            double flammability1 = getHexAt(vegetationHexes[index].y, vegetationHexes[index].x).getVegetation()
                .getFlammability();
            if (Random.Range(0.0f, 1.0f) < flammability1 * FIRE_SPREAD)
            {
                getHexAt(vegetationHexes[index].y, vegetationHexes[index].x).setBurning(true);
                updateMapVisuals();
            }
        }
        
        
        //spread
        foreach (Hex hex in hexes)
        {
            // if no neighbour is burning, there is no spread
            if (!hex.getNeighbours().Any(neighbour => neighbour.isBurning())) continue;
            
            // calculate fire spread
            double flammability = hex.getVegetation().getFlammability();
            if (Random.Range(0.0f, 1.0f) < flammability * FIRE_SPREAD)
            {
                hex.setBurning(true);
                updateMapVisuals();
                return;
            }
        }
    }

    public void doInfestationTick()
    {
        
    }

    public void doCO2Tick()
    {
        co2change = 0.0;
        foreach (var hex in hexes)
        {
            if (!hex.hasVegetation()) continue;
            
            // burning tiles dont consume co2
            if (hex.isBurning()) continue;

            if (hex.isInfested())
            {
                // TODO: infested tiles consume less co2
                co2change += hex.getVegetation().getCO2Usage() * INFESTATION_MULT;
                continue;
            }
            
            // normal consumption
            co2change += hex.getVegetation().getCO2Usage();
        }

        co2 -= co2change;
    }
}
