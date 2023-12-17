using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    string deathKey = "Deaths";
    string coinKey = "Coins";

    public int currentDeaths { get; set; }
    public int currentCoins { get; set; }

    private void Awake()
    {
        currentDeaths = PlayerPrefs.GetInt(deathKey);
        currentCoins = PlayerPrefs.GetInt(coinKey);
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
