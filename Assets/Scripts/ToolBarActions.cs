using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ToolBarActions : MonoBehaviour
{
    public HighlightController highlightController;
    public GameObject bottomRightToolTip;

    // Start is called before the first frame update
    void Start()
    {
        bottomRightToolTip.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToolBarClick(int index)
    {
        switch (index)
        {
            case 0: // Grass
                highlightController.veggieToPlant = new Grass();
                break;

            case 1: // Shrub
                highlightController.veggieToPlant = new Shrub();
                break;

            case 2: // Tree 1 // Leaf
                highlightController.veggieToPlant = new LeafTree();
                break;

            case 3: // Tree 2 // Fir
                highlightController.veggieToPlant = new FirTree();
                break;

            default:
                break;
        }
    }

    public void ToolBarPointerEnter(int index)
    {
        //switch (index)
        //{
        //    case 0: // Grass
        //        highlightController.veggieToPlant = new Grass();
        //        break;

        //    case 1: // Shrub
        //        highlightController.veggieToPlant = new Shrub();
        //        break;

        //    case 2: // Tree 1 // Leaf
        //        highlightController.veggieToPlant = new LeafTree();
        //        break;

        //    case 3: // Tree 2 // Fir
        //        highlightController.veggieToPlant = new FirTree();
        //        break;

        //    default:
        //        break;
        //}

        bottomRightToolTip.SetActive(true);
    }

    public void ToolBarPointerExit(int index)
    {
        if (highlightController.veggieToPlant == null) bottomRightToolTip.SetActive(false);
    }

}
