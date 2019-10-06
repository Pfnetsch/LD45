using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Resume();
        if(Input.GetKeyDown(KeyCode.Escape) && gameIsPaused)
        {
            Resume();
        }
    }

    public void Resume() // set to public in order to access via a button
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;            // set time to 0 to freeze time
        gameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;            // set time to 0 to freeze time
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        // set time back to normal
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void SaveGame()
    {
        // to be implemented
        Debug.Log("To be implemented");
    }

    public void PauseButtonClicked()
    {
        Pause();
    }
}
