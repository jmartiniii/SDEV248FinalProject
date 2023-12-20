using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FailScore : MonoBehaviour
{
    public TMP_Text failScore;
    private void Awake()
    {
        // gather the death and coin counts for display
        int totalDeaths = FindAnyObjectByType<ScoreCounter>().currentDeaths;

        // change the display text message based on the amount of knights lost during gameplay
        if (totalDeaths == 1)
        {
            failScore.SetText("You lost your gold and 1 Knight to the darkness!");
        }
        else if (totalDeaths > 1)
        {
            failScore.SetText("You lost your gold and " + totalDeaths + " Knights to the darkness!");
        }
        else
        {
            failScore.SetText("You broke the game!!");
        }
        
    }

    public void PlayAgain()
    {
        FindObjectOfType<ScoreCounter>().SetFader(0);
        FindObjectOfType<ScoreCounter>().SetTimer(60);
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
