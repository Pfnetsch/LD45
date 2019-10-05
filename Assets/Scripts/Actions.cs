using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
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
            HighlightController highLightControl = GetComponent<HighlightController>();
            Vegetation treeZeroSprite = new LeafTree();

            highLightControl.highlightTile = treeZeroSprite.getTileForLevel(0);
        }
    }


}
