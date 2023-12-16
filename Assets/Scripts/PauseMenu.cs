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
            if(Paused)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        PauseMenuCanvas.SetActive(true);
        PauseTime();
        Paused = true;
    }

    public void Play()
    {
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
        Time.timeScale = 1f;
    }

    private void PauseTime()
    {
        Time.timeScale = 0f;
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
