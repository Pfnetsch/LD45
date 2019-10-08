using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using DefaultNamespace;
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

    public const double FIRE_SPREAD = 0.15;
    public const double FIRE_START = 0.02;

    public const double INFESTATION_SPREAD = 0.1;
    public const double INFESTATION_MULT = 0.5;
    public const double INFESTATION_START = 0.02;

    public const double GROW_MULT = 0.2;

    public const int MAX_GAME_TIME_YEARS = 40;

    private static bool firstVeggieTextShown = false;
    private static bool firstLightningTextShown = false;
    private static bool firstInfestationTextShown = false;

    public Tile defaultTile;

    private Tile _fireTile1;
    private Tile _fireTile2;
    private Tile _bugTile1;
    private Tile _bugTile2;

    public int numRows;
    public int numColumns;
    private int startRow = 0;
    private int startColumn = 0;

    // graphic tiles
    private Grid grid;
    public Tilemap overlayTilemap;
    public Tilemap foregroundTilemap;
    public Tilemap backgroundTilemap;


    private PopUpInfo popUpInfo;

    // hexdata
    private Hex[,] hexes;
    
    // states
    private Boolean lightning = true;
    private Boolean infestation = true;
    
    // global co2
    private double co2Goal = 10000.0;
    private double co2change = 0.0;

    public DateTime date;
    private DateTime endDate;

    // Start is called before the first frame update
    void Start()
    {
        date = new DateTime(2019, 10, 7);
        endDate = date.AddYears(MAX_GAME_TIME_YEARS);

        Vegetation.setSeedsOrSaplings(typeof(Grass), 4);
        Vegetation.setSeedsOrSaplings(typeof(Shrub), 2);
        Vegetation.setSeedsOrSaplings(typeof(LeafTree), 1);
        Vegetation.setSeedsOrSaplings(typeof(FirTree), 1);
        Vegetation.setSeedsOrSaplings(typeof(Cactus), 1);

        _fireTile1 = Resources.Load<Tile>("Tiles/Feuer1_1");
        _fireTile2 = Resources.Load<Tile>("Tiles/Feuer1_2");
        _bugTile1 = Resources.Load<Tile>("Tiles/Bug1");
        _bugTile2 = Resources.Load<Tile>("Tiles/Bug2");

        popUpInfo = FindObjectOfType<PopUpInfo>();
        popUpInfo.gameObject.SetActive(false);

        grid = FindObjectOfType<Grid>();
        generateMap();
        CenterMainCameraOnGrid();
    }

    public double getCO2Level()
    {
        return 1 - co2change / co2Goal;
    }

    public DateTime getEndDate()
    {
        return endDate;
    }

    public DateTime getDate()
    {
        // map 20min to 40yr
        //double elapsed = (now - startDate).TotalSeconds * 1614400;
        
        if (isTimeElapsed())
        {
            Time.timeScale = 0;
            if (co2change > co2Goal)
                popUpInfo.ShowGameFinishedInfoText();
            else
                popUpInfo.ShowGameOverInfoText();
        }

        return date;
    }

    public Boolean isTimeElapsed()
    {
        return date > endDate;
    }

    public void CenterMainCameraOnGrid()
    {
        float gridWidth = numColumns * grid.cellSize.x / 2f;
        float gridHeight = numRows * grid.cellSize.y / 2f;

        Camera.main.transform.Translate(new Vector3(gridWidth, gridHeight, 0));
        Camera.main.GetComponent<cameraRTS>().SetGridSizeAndStoreInitialPosition(gridWidth, gridHeight);
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

        setBackgroundVisuals();
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

    public void setBackgroundVisuals()
    {
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Vector3Int pos = new Vector3Int(row + startRow, column + startColumn, 0);
                backgroundTilemap.SetTile(pos, defaultTile);
                backgroundTilemap.SetTileFlags(pos, TileFlags.None);
                backgroundTilemap.SetColor(pos, new Color(0xFF / 255.0f, 0xDA / 255.0f, 0x83 / 255.0f));
            }
        }
    }
    
    public void updateBackgroundVisuals()
    {
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex currentHex = hexes[column, row];
                Color tileColor;
                Vector3Int pos = new Vector3Int(row + startRow, column + startColumn, 0);

                if (currentHex.terrainType == Hex.TERRAIN_TYPE.OCEAN)
                {
                    //6BDEFF
                    tileColor = new Color(0x6b / 255.0f, 0xde / 255.0f, 0xff / 255.0f);
                }
                else if (currentHex.terrainType == Hex.TERRAIN_TYPE.DESERT)
                {
                    double waterLevel = currentHex.getWaterLevel();
                    // dynamic color
                    tileColor = Color.Lerp(new Color(0xFF / 255.0f, 0xDA / 255.0f, 0x83 / 255.0f), new Color(0x99 / 255.0f, 0x70 / 255.0f, 0x1C / 255.0f), (float)waterLevel + 0.2f);
                }
                else
                {
                    tileColor = Color.magenta;
                }

                backgroundTilemap.SetColor(pos, tileColor);
            }
        }
    }

    public void updateMapVisuals()
    {
        foregroundTilemap.ClearAllTiles();
        overlayTilemap.ClearAllTiles();

        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex currentHex = hexes[column, row];
                Vector3Int pos = new Vector3Int(row + startRow, column + startColumn, 0);

                // tile not empty => render foreground
                if (!hexes[column, row].isEmpty()) foregroundTilemap.SetTile(pos, currentHex.getCurrentTile());

                if (currentHex.isBurning())
                {
                    overlayTilemap.SetTile(pos, Random.value >= 0.5 ? _fireTile1 : _fireTile2);
                }
                else if (currentHex.isInfested())
                {
                    overlayTilemap.SetTile(pos, Random.value >= 0.5 ? _bugTile1 : _bugTile2);
                }
            }
        }
    }

    public void upgradeTile(Vector3Int position)
    {
        if (position.x >= this.startColumn && position.y >= this.startRow && position.x < this.startColumn + this.numColumns && position.y < this.startRow + this.numRows)
        {
            this.hexes[position.y, position.x].upgrade();
            this.updateMapVisuals();
        }
    }

    public Boolean canGrow(Vector3Int position, Vegetation vegetation)
    {
        Hex hex = getHexAt(position.y, position.x);

        if (hex == null)
            return false;

        if (hex.hasVegetation())
            return false;

        if (hex.isWaterSource())
            return false;

        // Not needed because hex should be null already
        //if (position.x >= this.startColumn && position.y >= this.startRow && position.x < this.startColumn + this.numColumns && position.y < this.startRow + this.numRows)
        //{
            //return hex.getWaterLevel() >= vegetation.getWaterRequirement();
        //}

        return hex.getWaterLevel() >= vegetation.getWaterRequirement();
    }
    
    public bool plantVegetation(Vector3Int position, Vegetation vegetation)
    {
        Hex hex = getHexAt(position.y, position.x);

        if (hex == null)
            return false;

        if (canGrow(position, vegetation))
        {
            this.hexes[position.y, position.x].setVegetation(vegetation);
            this.updateMapVisuals();

            if (!firstVeggieTextShown)
            {
                popUpInfo.ShowFirstPlantInfoText();
                firstVeggieTextShown = true;
            }

            return true;
        }

        return false;
    }

    public void doWaterTick()
    {
        double[,] newWater = new double[numColumns, numRows];
        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                Hex currentHex = hexes[column, row];
                Hex[] neighbours = currentHex.getNeighbours();
                double newWaterLevel = currentHex.getWaterLevel();

                foreach (Hex neighbour in neighbours)
                {
                    if (neighbour.getWaterLevel() > newWaterLevel)
                    {
                        newWaterLevel = neighbour.getWaterLevel();
                        //newWaterLevel = Math.Min(neighbour.getWaterLevel(), neighbour.getWaterLevel() * WATER_SPREAD + (neighbour.hasVegetation() ? ( neighbour.getVegetation().getWaterMod() * 0.1) : 0));
                    }
                }

                // TODO: adjust water spread formula
                newWater[column, row] = Math.Min(newWaterLevel, newWaterLevel * WATER_SPREAD + (currentHex.hasVegetation() ? ( currentHex.getVegetation().getWaterMod() * 0.5) : 0));
                //newWater[column, row] = newWaterLevel;
            }
        }

        for (int column = 0; column < numColumns; column++)
        {
            for (int row = 0; row < numRows; row++)
            {
                hexes[column, row].setWaterLevel(newWater[column, row]);
            }
        }
    }

    public void doFireTick()
    {
        List<Hex> vegetationHexes = (from Hex hex in hexes where hex.hasVegetation() select hex).ToList();

        // new fire?
        if (lightning)
        {
            if (vegetationHexes.Count > 0)
            {
                // select random tile and try to spread lightning
                int index = Random.Range(0, vegetationHexes.Count);

                // set on fire?
                double flammability1 = vegetationHexes[index].getVegetation().getFlammability();

                if (Random.Range(0.0f, 1.0f) < flammability1 * FIRE_START)
                {
                    vegetationHexes[index].setBurning(true);

                    if (!firstLightningTextShown)
                    {
                        popUpInfo.ShowFirstLightningInfoText();
                        firstLightningTextShown = true;
                    }
                }
            }
        }
        
        //spread
        foreach (Hex hex in vegetationHexes)
        {
            // if no neighbour is burning, there is no spread
            if (!hex.getNeighbours().Any(neighbour => neighbour.isBurning())) continue;

            // calculate fire spread
            double flammability = hex.getVegetation().getFlammability();

            if (Random.Range(0.0f, 1.0f) < flammability * FIRE_SPREAD)
            {
                hex.setBurning(true);
                return;
            }
        }
    }

    public void doInfestationTick()
    {
        List<Hex> vegetationHexes = (from Hex hex in hexes where hex.hasVegetation() select hex).ToList();

        // new fire?
        if (infestation)
        {
            if (vegetationHexes.Count > 0)
            {
                // select random tile and try to spread lightning
                int index = Random.Range(0, vegetationHexes.Count);

                // set on fire?
                double infestability = vegetationHexes[index].getVegetation().getInfestability();

                if (Random.Range(0.0f, 1.0f) < infestability * INFESTATION_START)
                {
                    vegetationHexes[index].setInfested(true);

                    if (!firstInfestationTextShown)
                    {
                        popUpInfo.ShowFirstInfestationInfoText();
                        firstInfestationTextShown = true;
                    }
                }
            }
        }

        //spread
        foreach (Hex hex in vegetationHexes)
        {
            // if no neighbour is burning, there is no spread
            if (!hex.getNeighbours().Any(neighbour => neighbour.isInfested())) continue;

            // calculate fire spread
            double infestability = hex.getVegetation().getInfestability();

            if (Random.Range(0.0f, 1.0f) < infestability * INFESTATION_SPREAD)
            {
                hex.setInfested(true);
                return;
            }
        }
    }

    public void doDeathTick()
    {
        List<Hex> vegetationHexes = (from Hex hex in hexes where hex.hasVegetation() select hex).ToList();

        foreach (Hex hex in vegetationHexes)
        {
            if (hex.isBurning() || hex.isInfested())
            {
                hex.doDeathTick();
            }
        }
    }

    public void doCO2Tick()
    {
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
    }

    public void doGrowTick()
    {
        foreach (var hex in hexes)
        {
            if (!hex.hasVegetation()) continue;

            if (hex.isMaxLevel()) continue;
            
            hex.doGrowTick();
        }
    }
}
