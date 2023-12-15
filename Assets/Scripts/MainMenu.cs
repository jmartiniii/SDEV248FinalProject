using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        FindObjectOfType<ScoreCounter>().SetDeaths(0);
        FindObjectOfType<ScoreCounter>().SetCoins(0);
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
