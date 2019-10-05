using UnityEngine;
using UnityEngine.Tilemaps;

public class Hex
{
    public enum TERRAIN_TYPE { DEFAULT, PLAINS, MARSH, DESERT, OCEAN }

    public TERRAIN_TYPE terrainType;

    public readonly HexMap HexMap;
    
    public Hex(HexMap hexMap, TERRAIN_TYPE terrainType = TERRAIN_TYPE.DEFAULT)
    {
        this.HexMap = hexMap;
        this.terrainType = terrainType;
    }
    
}
