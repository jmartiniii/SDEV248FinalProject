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
        int deaths = FindAnyObjectByType<ScoreCounter>().CurrentDeaths;
        int coins = FindAnyObjectByType<ScoreCounter>().CurrentCoins;

        finalScore.SetText("You retrieved " + coins + " coins and at the cost of " + deaths + " knight(s)!");
    }

    public void PlayAgain()
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
