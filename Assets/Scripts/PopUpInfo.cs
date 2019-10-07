using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpInfo : MonoBehaviour
{
    public TextAsset textFirstVeggie;
    public TextAsset textGameOver;
    public TextAsset textGameFinished;

    public Text popUpText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowFirstPlantInfoText()
    {
        gameObject.SetActive(true);
        popUpText.text = textFirstVeggie.text;       
    }

    public void ShowGameOverInfoText()
    {
        gameObject.SetActive(true);
        popUpText.text = textGameOver.text;
    }

    public void ShowGameFinishedInfoText()
    {
        gameObject.SetActive(true);
        popUpText.text = textGameFinished.text;
    }

    public void OkButtonClicked()
    {
        gameObject.SetActive(false);
    }
}
