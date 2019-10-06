using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class HighlightController : MonoBehaviour
{
	public Tilemap highlightTilemap;
    public Tilemap topTilemap;
	public Tile highlightTile;
    public Vegetation veggieToPlant;
	public Boolean highlightActive = false;

    Grid grid;
	private HexMap hexMap;

	private Vector3Int lastTilePos;

	private Boolean buttonDown = false;

	// Start is called before the first frame update
	void Start()
    {
		grid = FindObjectOfType<Grid>();
		hexMap = FindObjectOfType<HexMap>();
    }

    // Update is called once per frame
    void Update()
    {
	    if (highlightActive == false)
	    {
		    return;
	    }

	    // get tile for mousepos
	    Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    Vector3Int posInt = grid.LocalToCell(pos);

	    if (lastTilePos != posInt)
	    {
		    // tile changed, unhighlight last tile
		    highlightTilemap.SetTile(lastTilePos, null);

            if (veggieToPlant != null)
            {
                // if veggi is selected
                highlightTilemap.SetTile(posInt, veggieToPlant.getTileForLevel(0));
                // Debug.Log("Here 1");
                Debug.Log(!hexMap.canGrow(posInt, veggieToPlant));
                if (!hexMap.canGrow(posInt, veggieToPlant))
                {
                    Debug.Log("Here 2");
                    topTilemap.SetTileFlags(posInt, TileFlags.None);
                    topTilemap.SetColor(posInt, Color.white);
                }
            } 
            else
            {
                highlightTilemap.SetTile(posInt, highlightTile);
                highlightTilemap.SetColor(posInt, Color.black);
            }

            lastTilePos = posInt;
	    }

	    if (Input.GetMouseButtonDown(0) && buttonDown == false)
	    {
		    buttonDown = true;

		    Debug.Log(posInt);
		    
            if (veggieToPlant != null)
            {
                hexMap.plantVegetation(posInt, veggieToPlant);
                veggieToPlant = null;
            }
            else
                hexMap.upgradeTile(posInt);
        }

	    if (Input.GetMouseButtonUp(0) && buttonDown)
	    {
		    buttonDown = false;
	    }
    }
}
