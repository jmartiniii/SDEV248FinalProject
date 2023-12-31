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
        int totalDeaths = FindAnyObjectByType<ScoreCounter>().currentDeaths;
        int totalCoins = FindAnyObjectByType<ScoreCounter>().currentCoins;

        // change the display text message based on the amount of knights lost during gameplay
        if (totalDeaths < 1)
        {
            finalScore.SetText("You retrieved " + totalCoins + " coins without losing a single Knight!");
        }
        else if (totalDeaths == 1)
        {
            finalScore.SetText("You retrieved " + totalCoins + " coins at the cost of " + totalDeaths + " Knight!");
        }
        else
        {
            finalScore.SetText("You retrieved " + totalCoins + " coins at the cost of " + totalDeaths + " Knights!");
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
