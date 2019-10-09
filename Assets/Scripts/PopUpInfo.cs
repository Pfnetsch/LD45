using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PopUpInfo : MonoBehaviour
{
    public TextAsset textFirstVeggie;
    public TextAsset textFirstLightning;
    public TextAsset textFirstInfestation;

    public TextAsset textGameOver;
    public TextAsset textGameFinished;

    public Text popUpText;

    private bool gameIsOver = false;

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
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        popUpText.text = textFirstVeggie.text;
    }

    public void ShowFirstLightningInfoText()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        popUpText.text = textFirstLightning.text;
    }

    public void ShowFirstInfestationInfoText()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        popUpText.text = textFirstInfestation.text;
    }

    public void ShowGameOverInfoText()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        popUpText.text = textGameOver.text;
        gameIsOver = true;
    }

    public void ShowGameFinishedInfoText()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
        popUpText.text = textGameFinished.text;
        gameIsOver = true;
    }

    public void OkButtonClicked()
    {
        // set time back to normal
        Time.timeScale = 1f;

        if (!gameIsOver)
            gameObject.SetActive(false);
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
