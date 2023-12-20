using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuCanvas;
    public GameObject HelpCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PauseResume();
        }
    }

    private void PauseResume()
    {
        if (HelpCanvas.activeSelf)
        {
            HelpCanvas.SetActive(false);
            PauseMenuCanvas.SetActive(true);
        }
        else if (PauseMenuCanvas.activeSelf)
        {
            PauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
            PauseMenuCanvas.SetActive(true);
        }
    }

    public void MainMenuButton()
    {
        // load the main menu
        SceneManager.LoadScene("MainMenu");
    }

}
