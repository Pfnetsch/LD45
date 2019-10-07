using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ToolBarActions : MonoBehaviour
{
    public HighlightController highlightController;
    public HexMap hexMap;
    public GameObject toolTipList;

    public GameObject buttonGrass;
    public GameObject buttonShrub;
    public GameObject buttonLeafTree;
    public GameObject buttonFirTree;

    private Vegetation _veggie;
    private Text textGrass;
    private Text textShrub;
    private Text textLeafTree;
    private Text textFirTree;

    // Start is called before the first frame update
    void Start()
    {
        toolTipList.SetActive(false);

        textGrass = buttonGrass.GetComponentInChildren<Text>();
        textShrub = buttonShrub.GetComponentInChildren<Text>();
        textLeafTree = buttonLeafTree.GetComponentInChildren<Text>();
        textFirTree = buttonFirTree.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int grass = Grass.getSeedsOrSaplings(typeof(Grass));
        int shrub = Shrub.getSeedsOrSaplings(typeof(Shrub));
        int leafTree = LeafTree.getSeedsOrSaplings(typeof(LeafTree));
        int firTree = Grass.getSeedsOrSaplings(typeof(FirTree));

        textGrass.text = grass.ToString();
        textShrub.text = shrub.ToString();
        textLeafTree.text = leafTree.ToString();
        textFirTree.text = firTree.ToString();

        if (grass == 0) buttonGrass.GetComponent<Button>().interactable = false;
        else buttonGrass.GetComponent<Button>().interactable = true;

        if (shrub == 0) buttonShrub.GetComponent<Button>().interactable = false;
        else buttonShrub.GetComponent<Button>().interactable = true;

        if (leafTree == 0) buttonLeafTree.GetComponent<Button>().interactable = false;
        else buttonLeafTree.GetComponent<Button>().interactable = true;

        if (firTree == 0) buttonFirTree.GetComponent<Button>().interactable = false;
        else buttonFirTree.GetComponent<Button>().interactable = true;
    }

    public void ToolBarClick(int index)
    {
        if (_veggie.SeedsOrSaplings > 0)
        {
            switch (index)
            {
                case 0: // Grass
                    highlightController.veggieToPlant = _veggie;
                    break;

                case 1: // Shrub
                    highlightController.veggieToPlant = _veggie;
                    break;

                case 2: // Tree 1 // Leaf
                    highlightController.veggieToPlant = _veggie;
                    break;

                case 3: // Tree 2 // Fir
                    highlightController.veggieToPlant = _veggie;
                    break;

                default:
                    break;
            }
        }
    }

    public void ToolBarPointerEnter(int index)
    {
        switch (index)
        {
            case 0: // Grass
                _veggie = new Grass();
                break;

            case 1: // Shrub
                _veggie = new Shrub();
                break;

            case 2: // Tree 1 // Leaf
                _veggie = new LeafTree();
                break;

            case 3: // Tree 2 // Fir
                _veggie = new FirTree();
                break;

            default:
                break;
        }

        toolTipList.GetComponent<ToolTipList>().Veggie = _veggie;
        toolTipList.GetComponent<ToolTipList>().locked = true;
    }

    public void ToolBarPointerExit(int index)
    {
        toolTipList.GetComponent<ToolTipList>().locked = false;

        if (highlightController.veggieToPlant == null)
        {
            toolTipList.SetActive(false);
        }
    }

}
