using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private static bool Paused = false;
    public GameObject PauseMenuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        ResumeTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!Paused)
            {
                Stop();
            }
            else
            {
                Play();
            }
        }
    }

    void Stop()
    {
        // enable the pause menu, pause time, and change the pausse to true
        PauseMenuCanvas.SetActive(true);
        PauseTime();
        Paused = true;
    }

    public void Play()
    {
        // disable the pause menu, resume time, and change paused to false
        PauseMenuCanvas.SetActive(false);
        ResumeTime();
        Paused = false;
    }

    public void MainMenuButton()
    {
        Play();
        GoToMainMenu();
    }

    private void ResumeTime()
    {
        // set the game time back to normal
        Time.timeScale = 1f;
    }

    private void PauseTime()
    {
        // set the game time to zero effectively pausing gameplay
        Time.timeScale = 0f;
    }

    private void GoToMainMenu()
    {
        // load the main menu
        SceneManager.LoadScene("MainMenu");
    }

}
