using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class HighlightController : MonoBehaviour
{
    public Tilemap highlightTilemap;
	public Tile highlightTile;
	public Tile notAllowedTile;
    public Vegetation veggieToPlant;
	public Boolean highlightActive = false;

    private ToolTipList tooltipList;

    private Grid grid;
    private HexMap hexMap;
	private Vector3Int lastTilePos;
	private Boolean buttonDown = false;

	// Start is called before the first frame update
	void Start()
    {
        grid = FindObjectOfType<Grid>();
        hexMap = FindObjectOfType<HexMap>();
        tooltipList = FindObjectOfType<ToolTipList>();
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
        Hex hex = hexMap.getHexAt(posInt.y, posInt.x);

        if (lastTilePos != posInt)
	    {
		    // tile changed, unhighlight last tile
		    highlightTilemap.SetTile(lastTilePos, null);

		    if (veggieToPlant != null)
		    {
			    if (!hexMap.canGrow(posInt, veggieToPlant))
			    {
				    highlightTilemap.SetTile(posInt, notAllowedTile); 
			    }
			    else
			    {
				    highlightTilemap.SetTile(posInt, veggieToPlant.getTileForLevel(0));
			    }
		    }
		    else
            {
                if (hex != null) highlightTilemap.SetTile(posInt, highlightTile);
            }

            tooltipList.Hex = hex;
            lastTilePos = posInt;

            if (Input.GetMouseButton(0) && veggieToPlant == null && hex != null) hex.harvestVegetation();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            veggieToPlant = null;
            highlightTilemap.SetTile(posInt, highlightTile);
        }

        if (Input.GetMouseButtonDown(0) && buttonDown == false)
	    {
            buttonDown = true;

            //Debug.Log(posInt);

            if (veggieToPlant != null && hexMap.canGrow(posInt, veggieToPlant))
            {
                if (hexMap.plantVegetation(posInt, veggieToPlant)) veggieToPlant.SeedsOrSaplings--;

                if (veggieToPlant.SeedsOrSaplings == 0) veggieToPlant = null;
            }
            else if (hex != null) hex.harvestVegetation();

        }

	    if (Input.GetMouseButtonUp(0) && buttonDown)
	    {
		    buttonDown = false;
	    }
    }
}
