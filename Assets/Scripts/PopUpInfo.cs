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
        gameObject.SetActive(true);
        popUpText.text = textFirstVeggie.text;
        gameIsOver = true;
    }

    public void ShowGameOverInfoText()
    {
        gameObject.SetActive(true);
        popUpText.text = textGameOver.text;
        gameIsOver = true;
    }

    public void ShowGameFinishedInfoText()
    {
        gameObject.SetActive(true);
        popUpText.text = textGameFinished.text;
    }

    public void OkButtonClicked()
    {
        if (!gameIsOver)
            gameObject.SetActive(false);
        else
        {
            // set time back to normal
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu");
        }
    }
}
