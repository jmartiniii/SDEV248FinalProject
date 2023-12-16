using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        // set the death and coin counts to zero
        FindObjectOfType<ScoreCounter>().SetDeaths(0);
        FindObjectOfType<ScoreCounter>().SetCoins(0);

        // load the next scene in the game (level1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        // quit
        Application.Quit();
    }
}
