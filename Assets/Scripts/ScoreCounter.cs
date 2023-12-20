using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private string timeTillDark = "TimeTillDark";
    private string timeToFinish = "TimeToFinish";
    private string deathKey = "Deaths";
    private string coinKey = "Coins";

    public float lightRemaining { get; set; }
    public float timeRemaining { get; set; }
    public int currentDeaths { get; set; }
    public int currentCoins { get; set; }

    private void Awake()
    {
        lightRemaining = PlayerPrefs.GetFloat(timeTillDark);
        timeRemaining = PlayerPrefs.GetFloat(timeToFinish);
        currentDeaths = PlayerPrefs.GetInt(deathKey);
        currentCoins = PlayerPrefs.GetInt(coinKey);
    }

    public void SetFader(float fader)
    {
        PlayerPrefs.SetFloat(timeTillDark, fader);
    }

    public void SetTimer(float timer)
    {
        PlayerPrefs.SetFloat(timeToFinish, timer);
    }

    public void SetDeaths(int deaths)
    {
        PlayerPrefs.SetInt(deathKey, deaths);
    }

    public void SetCoins(int coins)
    {
        PlayerPrefs.SetInt(coinKey, coins);
    }
}
