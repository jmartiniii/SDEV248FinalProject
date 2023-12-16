using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FinalScore : MonoBehaviour
{
    public TMP_Text finalScore;
    private void Awake()
    {
        // gather the death and coin counts for display
        int deaths = FindAnyObjectByType<ScoreCounter>().CurrentDeaths;
        int coins = FindAnyObjectByType<ScoreCounter>().CurrentCoins;

        // change the display text message based on the amount of knights lost during gameplay
        if (deaths == 0)
        {
            finalScore.SetText("You retrieved " + coins + " coins without losing a single knight!");
        }
        else if (deaths == 1)
        {
            finalScore.SetText("You retrieved " + coins + " coins at the cost of " + deaths + " knight!");
        }
        else
        {
            finalScore.SetText("You retrieved " + coins + " coins at the cost of " + deaths + " knights!");
        }
        
    }

    public void PlayAgain()
    {
        // reset the death and coin counts
        FindObjectOfType<ScoreCounter>().SetDeaths(0);
        FindObjectOfType<ScoreCounter>().SetCoins(0);
        // load to level1
        SceneManager.LoadScene("Level1");
    }

    public void Quit()
    {
        // quit
        Application.Quit();
    }
}
