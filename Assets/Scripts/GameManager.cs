using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Vector2 startPos;
    public GameObject playerGameObject;
    public GameObject corpsePrefab;

    ScoreCounter scoreCounter;

    private AudioSource gameAudio;
    public AudioClip coinSound;

    // night fade
    private float fadeTimer;
    public Image nightFade;

    // end of game timer
    private float roundTimer;
    public Text timerText;

    // coin count
    private int coins;
    public Text coinText;

    // death count
    private int deaths;
    public Text deathText;

    private bool recentlyDied = false;
    private bool timerActive = true;

    private void Start()
    {
        startPos = playerGameObject.transform.position;

        scoreCounter = FindObjectOfType<ScoreCounter>();
        gameAudio = GetComponent<AudioSource>();

        fadeTimer = scoreCounter.lightRemaining;

        roundTimer = scoreCounter.timeRemaining;
        timerText.text = roundTimer.ToString();

        coins = scoreCounter.currentCoins;
        coinText.text = coins.ToString();

        deaths = scoreCounter.currentDeaths;
        deathText.text = deaths.ToString();
    }

    private void Update()
    {
        TimerCountdown();
    }

    private void TimerCountdown()
    {
        if (timerActive)
        {
            if (roundTimer > 0)
            {
                RoundTimerCountdown();
                RoundFadeToBlack();
            }
            else
            {
                RoundTimerEmpty();
            }
        }
    }

    private void RoundFadeToBlack()
    {
        fadeTimer += Time.deltaTime;
        float darkFade = fadeTimer / 60;
        nightFade.color = new Color(0, 0, 0, darkFade);
    }

    private void RoundTimerCountdown()
    {
        roundTimer -= Time.deltaTime;
        int displayTimer = Mathf.RoundToInt(roundTimer);
        if (displayTimer < 11)
        {
            timerText.color = new Color(1, 0, 0);
        }
        timerText.text = displayTimer.ToString();
    }

    private void RoundTimerEmpty()
    {
        Die();
        UpdateTotals();
        Invoke(nameof(FailureSwitch), 3.0f);
    }

    public void AddToCoinTotal(int count)
    {
        gameAudio.PlayOneShot(coinSound, 0.03f);
        coins += count;
        coinText.text = coins.ToString();
    }

    public void AddToDeathTotal()
    {
        deaths += 1;
        deathText.text = deaths.ToString();
    }

    public void Die()
    {
        // set recently died true so multi death cannot occur
        // deactivate round timer and player, spawn corpse, add a death, then respawn
        if (!recentlyDied)
        {
            recentlyDied = true;
            timerActive = false;
            playerGameObject.SetActive(false);
            SpawnCorpse();
            AddToDeathTotal();
            Invoke(nameof(Respawn), 3.0f);
        }
    }

    private void SpawnCorpse()
    {
        // spawn a corpse
        GameObject deathPlayer = (GameObject)Instantiate(corpsePrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
        StartCoroutine(ShrinkLoop(deathPlayer));
        //deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
    }

    private IEnumerator ShrinkLoop(GameObject deathPlayer)
    {
        float i = 1;
        float endSize = 0.5f;
        float waitTime = 0.01f;
        float reduction = 0.01f;
        

        while (i > endSize)
        {
            deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z) * i;
            yield return new WaitForSeconds(waitTime);
            i -= reduction;
        }
    }

    private void Respawn()
    {
        // move back to original position
        // set the player and timer to active
        // set recently died to false to allow dying
        playerGameObject.transform.position = startPos;
        playerGameObject.SetActive(true);
        timerActive = true;
        recentlyDied = false;
    }

    public void UpdateTotals()
    {
        // pause and save round and darkness fade timer
        timerActive = false;
        scoreCounter.SetFader(fadeTimer);
        scoreCounter.SetTimer(roundTimer);
        // death and coin counters
        scoreCounter.SetDeaths(deaths);
        scoreCounter.SetCoins(coins);
    }

    public IEnumerator SceneSwitch()
    {
        // wait 2 seconds then load the next scene
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FailureSwitch()
    {
        // load failed screen
        SceneManager.LoadScene("FailScore");
    }
}
