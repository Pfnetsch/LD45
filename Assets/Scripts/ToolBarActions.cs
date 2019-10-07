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
    public GameObject leftInfoBox;

    public TextAsset infoTextTree;
    public TextAsset infoTextShrub;
    public TextAsset infoTextGrassland;
    public TextAsset infoTextCactus;

    public GameObject buttonGrass;
    public GameObject buttonShrub;
    public GameObject buttonLeafTree;
    public GameObject buttonFirTree;
    public GameObject buttonCactus;

    private Vegetation _veggie;
    private Text countGrass;
    private Text countShrub;
    private Text countLeafTree;
    private Text countFirTree;
    private Text countCactus;

    // Start is called before the first frame update
    void Start()
    {
        countGrass = buttonGrass.GetComponentInChildren<Text>();
        countShrub = buttonShrub.GetComponentInChildren<Text>();
        countLeafTree = buttonLeafTree.GetComponentInChildren<Text>();
        countFirTree = buttonFirTree.GetComponentInChildren<Text>();
        countCactus = buttonCactus.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int grass = Grass.getSeedsOrSaplings(typeof(Grass));
        int shrub = Shrub.getSeedsOrSaplings(typeof(Shrub));
        int leafTree = LeafTree.getSeedsOrSaplings(typeof(LeafTree));
        int firTree = Grass.getSeedsOrSaplings(typeof(FirTree));
        int cactus = Cactus.getSeedsOrSaplings(typeof(Cactus));

        countGrass.text = grass.ToString();
        countShrub.text = shrub.ToString();
        countLeafTree.text = leafTree.ToString();
        countFirTree.text = firTree.ToString();
        countCactus.text = cactus.ToString();

        if (grass == 0) buttonGrass.GetComponent<Button>().interactable = false;
        else buttonGrass.GetComponent<Button>().interactable = true;

        if (shrub == 0) buttonShrub.GetComponent<Button>().interactable = false;
        else buttonShrub.GetComponent<Button>().interactable = true;

        if (leafTree == 0) buttonLeafTree.GetComponent<Button>().interactable = false;
        else buttonLeafTree.GetComponent<Button>().interactable = true;

        if (firTree == 0) buttonFirTree.GetComponent<Button>().interactable = false;
        else buttonFirTree.GetComponent<Button>().interactable = true;

        if (cactus == 0) buttonCactus.GetComponent<Button>().interactable = false;
        else buttonCactus.GetComponent<Button>().interactable = true;
    }

    public void ToolBarClick(int index)
    {
        if (_veggie.SeedsOrSaplings > 0)
        {
            highlightController.veggieToPlant = _veggie;
        }
    }

    public void ToolBarPointerEnter(int index)
    {
        switch (index)
        {
            case 0: // Grass
                _veggie = new Grass();
                leftInfoBox.GetComponentInChildren<Text>().text = infoTextGrassland.text;
                break;

            case 1: // Shrub
                _veggie = new Shrub();
                leftInfoBox.GetComponentInChildren<Text>().text = infoTextShrub.text;
                break;

            case 2: // Tree 1 // Leaf
                _veggie = new LeafTree();
                leftInfoBox.GetComponentInChildren<Text>().text = infoTextTree.text;
                break;

            case 3: // Tree 2 // Fir
                _veggie = new FirTree();
                leftInfoBox.GetComponentInChildren<Text>().text = infoTextTree.text;
                break;
            
            case 4: // Cactus
                _veggie = new Cactus();
                leftInfoBox.GetComponentInChildren<Text>().text = infoTextCactus.text;
                break;

            default:
                break;
        }

        toolTipList.GetComponent<ToolTipList>().Veggie = _veggie;
        toolTipList.GetComponent<ToolTipList>().locked = true;

        leftInfoBox.SetActive(true);
    }

    public void ToolBarPointerExit(int index)
    {
        toolTipList.GetComponent<ToolTipList>().locked = false;

        leftInfoBox.SetActive(false);
    }
}
