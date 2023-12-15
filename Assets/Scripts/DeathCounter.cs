using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    string deathKey = "Deaths";

    public int CurrentDeaths { get; set; }

    private void Awake()
    {
        CurrentDeaths = PlayerPrefs.GetInt(deathKey);
    }

    public void SetDeaths(int deaths)
    {
        PlayerPrefs.SetInt(deathKey, deaths);
    }
}
