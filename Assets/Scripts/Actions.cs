using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Actions : MonoBehaviour
{
    public HighlightController highLightController;
    public Tile highLightTile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropDownGrassIndexChanged(int index)
    {

    }

    public void DropDownBushIndexChanged(int index)
    {

    }

    public void DropDownTreeIndexChanged(int index)
    {
        if (index == 0)
        {
            Vegetation treeZeroSprite = new LeafTree();
            highLightController.highlightTile = treeZeroSprite.getTileForLevel(0);
        }
        else if (index == 1)
        {
            highLightController.highlightTile = highLightTile;
        }
    }


}
