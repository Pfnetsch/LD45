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
		    highlightTilemap.SetTile(posInt, highlightTile);
		    lastTilePos = posInt;
	    }

	    if (Input.GetMouseButtonDown(0) && buttonDown == false)
	    {
		    buttonDown = true;
		    hexMap.upgradeTile(posInt);
	    }

	    if (Input.GetMouseButtonUp(0) && buttonDown)
	    {
		    buttonDown = false;
	    }
    }
}
