using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Slider masterAudioSlider;

    public void Start()
    {
        masterAudioSlider.value = AudioListener.volume;
    }

    public void playGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void MasterAudioOnValueChanged(float value)
    {
        AudioListener.volume = value;
    }
}
