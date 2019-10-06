﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolTipList : MonoBehaviour
{
    public GameObject toolTipEntry;
    public bool locked;

    private Vegetation _veggie;
    private Hex _hex;

    // Veggie
    private GameObject _propWaterReq;
    private GameObject _propWaterMod;
    private GameObject _propGrowrate;
    private GameObject _propCO2usage;
    private GameObject _propFlammability;
    private GameObject _propInfestability;

    // Hex
    private GameObject _propWaterLevel;

    public Vegetation Veggie { get => _veggie;
        set
        {
            if (!locked)
            {
                _veggie = value;
                _hex = null;
                UpdateToolTipItems();
            }
        }
    }

    public Hex Hex { get => _hex;
        set
        {
            if (!locked)
            {
                _hex = value;
                _veggie = null;
                UpdateToolTipItems();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Veggie
        _propWaterReq = Instantiate(toolTipEntry, gameObject.transform, false);
        _propWaterReq.transform.localPosition = new Vector3(-100, 280, 0);
        _propWaterReq.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Wasserverbrauch.svg", typeof(Sprite)) as Sprite;
        _propWaterReq.GetComponent<ToolTipItem>().toolTipText.text = "Water needed";
        _propWaterReq.GetComponent<ToolTipItem>().revertColors = true;

        _propWaterMod = Instantiate(toolTipEntry, gameObject.transform, false);
        _propWaterMod.transform.localPosition = new Vector3(-100, 230, 0);
        _propWaterMod.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Wasserstand.svg", typeof(Sprite)) as Sprite;
        _propWaterMod.GetComponent<ToolTipItem>().toolTipText.text = "Water Storage";

        _propGrowrate = Instantiate(toolTipEntry, gameObject.transform, false);
        _propGrowrate.transform.localPosition = new Vector3(-100, 180, 0);
        _propGrowrate.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Zeitanzeige.svg", typeof(Sprite)) as Sprite;
        _propGrowrate.GetComponent<ToolTipItem>().toolTipText.text = "Growrate";

        _propCO2usage = Instantiate(toolTipEntry, gameObject.transform, false);
        _propCO2usage.transform.localPosition = new Vector3(-100, 130, 0);
        _propCO2usage.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/CO2_Anzeige.svg", typeof(Sprite)) as Sprite;
        _propCO2usage.GetComponent<ToolTipItem>().toolTipText.text = "C02 bound";

        _propFlammability = Instantiate(toolTipEntry, gameObject.transform, false);
        _propFlammability.transform.localPosition = new Vector3(-100, 80, 0);
        _propFlammability.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Feuerresistent.svg", typeof(Sprite)) as Sprite;
        _propFlammability.GetComponent<ToolTipItem>().toolTipText.text = "Fire Resistance";

        _propInfestability = Instantiate(toolTipEntry, gameObject.transform, false);
        _propInfestability.transform.localPosition = new Vector3(-100, 30, 0);
        _propInfestability.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Schädlingsresistent.svg", typeof(Sprite)) as Sprite;
        _propInfestability.GetComponent<ToolTipItem>().toolTipText.text = "Disease Resistance";

        // Hex
        _propWaterLevel = Instantiate(toolTipEntry, gameObject.transform, false);
        _propWaterLevel.transform.localPosition = new Vector3(-100, 30, 0);
        _propWaterLevel.GetComponent<ToolTipItem>().toolTipImage.sprite = AssetDatabase.LoadAssetAtPath("Assets/Sprites/Properties/Wasserstand.svg", typeof(Sprite)) as Sprite;
        _propWaterLevel.GetComponent<ToolTipItem>().toolTipText.text = "Water Level";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateToolTipItems()
    {
        if (_veggie != null)
        {
            locked = true;
            // Hex
            _propWaterLevel.SetActive(false);

            // Veggie
            _propWaterReq.SetActive(true);
            _propWaterReq.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getWaterRequirement();

            _propWaterMod.SetActive(true);
            _propWaterMod.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getWaterMod();

            _propGrowrate.SetActive(true);
            _propGrowrate.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getGrowrate();

            _propCO2usage.SetActive(true);
            _propCO2usage.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getCO2Usage();

            _propFlammability.SetActive(true);
            _propFlammability.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getFlammability();

            _propInfestability.SetActive(true);
            _propInfestability.GetComponent<ToolTipItem>().slider.value = (float)_veggie.getInfestability();

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 310);
        }
        else if (_hex != null)
        {
            // Veggie
            _propWaterReq.SetActive(false);
            _propWaterMod.SetActive(false);
            _propGrowrate.SetActive(false);
            _propCO2usage.SetActive(false);
            _propFlammability.SetActive(false);
            _propInfestability.SetActive(false);

            // Hex
            _propWaterLevel.SetActive(true);
            _propWaterLevel.GetComponent<ToolTipItem>().slider.value = (float)_hex.getWaterLevel();

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 60);
        }
    }
}