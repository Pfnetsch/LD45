using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolTipList : MonoBehaviour
{
    public GameObject toolTipEntry;

    private Vegetation _veggie;
    private Hex _hex;

    public Vegetation Veggie { get => _veggie;
        set
        {
            _veggie = value;
            _hex = null;
            UpdateToolTipItems();
        }
    }

    public Hex Hex { get => _hex;
        set
        {
            _hex = value;
            _veggie = null;
            UpdateToolTipItems();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateToolTipItems()
    {
        if (_veggie != null)
        {
            GameObject propWaterReq = Instantiate(toolTipEntry, gameObject.transform, false);
            propWaterReq.transform.localPosition = new Vector3(-100, 280, 0);
            propWaterReq.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Wasserverbrauch.svg", typeof(Sprite)) as Sprite;
            propWaterReq.GetComponent<ToolTipItem>().toolTipText.text = "Water needed";
            propWaterReq.GetComponent<ToolTipItem>().revertColors = true;
            propWaterReq.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getWaterRequirement();

            GameObject propWaterMod = Instantiate(toolTipEntry, gameObject.transform, false);
            propWaterMod.transform.localPosition = new Vector3(-100, 230, 0);
            propWaterMod.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Wasserstand.svg", typeof(Sprite)) as Sprite;
            propWaterMod.GetComponent<ToolTipItem>().toolTipText.text = "Water Storage";
            propWaterMod.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getWaterMod();

            GameObject propGrowrate = Instantiate(toolTipEntry, gameObject.transform, false);
            propGrowrate.transform.localPosition = new Vector3(-100, 180, 0);
            propGrowrate.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Zeitanzeige.svg", typeof(Sprite)) as Sprite;
            propGrowrate.GetComponent<ToolTipItem>().toolTipText.text = "Growrate";
            propGrowrate.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getGrowrate();

            GameObject propCO2usage = Instantiate(toolTipEntry, gameObject.transform, false);
            propCO2usage.transform.localPosition = new Vector3(-100, 130, 0);
            propCO2usage.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/CO2_Anzeige.svg", typeof(Sprite)) as Sprite;
            propCO2usage.GetComponent<ToolTipItem>().toolTipText.text = "C02 bound";
            propCO2usage.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getCO2Usage();

            GameObject propFlammability = Instantiate(toolTipEntry, gameObject.transform, false);
            propFlammability.transform.localPosition = new Vector3(-100, 80, 0);
            propFlammability.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Feuerresistent.svg", typeof(Sprite)) as Sprite;
            propFlammability.GetComponent<ToolTipItem>().toolTipText.text = "Fire Resistance";
            propFlammability.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getFlammability();

            GameObject propInfestability = Instantiate(toolTipEntry, gameObject.transform, false);
            propInfestability.transform.localPosition = new Vector3(-100, 30, 0);
            propInfestability.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Schädlingsresistent.svg", typeof(Sprite)) as Sprite;
            propInfestability.GetComponent<ToolTipItem>().toolTipText.text = "Disease Resistance";
            propInfestability.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getInfestability();

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 310);
        }
        else if (_hex != null)
        {

        }
    }
}
