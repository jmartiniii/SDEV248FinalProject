using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    string deathKey = "Deaths";
    string coinKey = "Coins";

    public int CurrentDeaths { get; set; }
    public int CurrentCoins { get; set; }

    private void Awake()
    {
        CurrentDeaths = PlayerPrefs.GetInt(deathKey);
        CurrentCoins = PlayerPrefs.GetInt(coinKey);
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
