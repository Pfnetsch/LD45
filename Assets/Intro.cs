using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public List<GameObject> stories;
    public List<GameObject> darkPanels;

    private int currentStory = 0;
    private int currentPanel = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            if (currentPanel < darkPanels.Count)
            {
                darkPanels[currentPanel].SetActive(false);
                currentPanel++;
            }      
            else
            {
                if (currentStory < stories.Count)
                {
                    darkPanels.ForEach(d => d.SetActive(true));
                    currentPanel = 0;
                    stories[currentStory].SetActive(false);
                    currentStory++;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) || currentStory == stories.Count)
        {
            FindObjectOfType<AudioScript>().SwitchIntoGameMode();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
