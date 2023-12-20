using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    public GameObject playerGameObject;
    public GameObject deadPrefab;

    // player audio and sound
    //private AudioSource playerAudio;
    //public AudioClip coinSound;

    // reference score counter
    ScoreCounter scoreCounter;
    GameManager gameManager;

    // night fade
    [SerializeField] float fadeTimer;
    public Image nightFade;

    // end of game timer
    [SerializeField] float roundTimer;
    public Text timerText;

    // coin count
    //[SerializeField] int coins;
    //public Text coinText;

    // death count
    //[SerializeField] int deaths;
    //public Text deathText;
    
    //private bool recentlyDied;
    private bool timerActive;



    // Start is called before the first frame update
    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        scoreCounter = FindObjectOfType<ScoreCounter>();
        gameManager = FindObjectOfType<GameManager>();

        startPos = transform.position;

        fadeTimer = scoreCounter.lightRemaining;

        roundTimer = scoreCounter.timeRemaining;
        timerText.text = roundTimer.ToString();

        //deaths = scoreCounter.currentDeaths;
        //deathText.text = deaths.ToString();
        //coins = scoreCounter.currentCoins;
        //coinText.text = coins.ToString();

        //recentlyDied = false;
        timerActive = true;
    }

    private void Update()
    {
        //gameManager.TimerCountdown();
    }

    private void OldTimerCountdown()
    {
        if (timerActive)
        {
            if (roundTimer > 0)
            {
                //gameManager.RoundTimerCountdown();
                //gameManager.FadeToBlack();
                //CountingDown();
                //CreepingDark();
            }
            else
            {
                OutOfTime();
            }
        }
    }

    private void CreepingDark()
    {
        fadeTimer += Time.deltaTime;
        float darkFade = fadeTimer / 60;
        nightFade.color = new Color(0, 0, 0, darkFade);
    }

    private void CountingDown()
    {
        roundTimer -= Time.deltaTime;
        int displayTime = Mathf.RoundToInt(roundTimer);
        if (displayTime < 11)
        {
            timerText.color = new Color(1, 0, 0);
        }
        timerText.text = displayTime.ToString();
    }

    private void OutOfTime()
    {
        playerGameObject.SetActive(false);
        SpawnCorpse();
        //AddDeath();
        //UpdateCounters();
        gameManager.UpdateTotals();
        Invoke(nameof(gameManager.FailureSwitch), 5.0f);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            gameManager.Die();
            // die and add death
            //if (!recentlyDied)
            //{
                //recentlyDied = true;
                //Die();
                //gameManager.AddToDeathTotal();
            //}
        }

        else if (collision.CompareTag("Coin"))
        {
            // add coins
            Destroy(collision.gameObject);
            gameManager.AddToCoinTotal(1);
        }

        else if (collision.CompareTag("Coin5"))
        {
            // add coins
            Destroy(collision.gameObject);
            gameManager.AddToCoinTotal(5);
        }

        else if (collision.CompareTag("SceneSwitch"))
        {
            // switch scene
            //UpdateCounters();
            gameManager.UpdateTotals();
            //StartCoroutine(SceneSwitch());
            StartCoroutine(gameManager.SceneSwitch());
        }

    }

    private IEnumerator SceneSwitch()
    {
        // wait 2 seconds then load the next scene
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void FailureSwitch()
    {
        SceneManager.LoadScene("FailScore");
    }


    private void Die() 
    {
        //deactivate player, spawn corpse, respawn
        playerGameObject.SetActive(false);
        SpawnCorpse();
        Invoke(nameof(Respawn), 5.0f);
    }

    private void AddCoin(int count)
    {
        //add to coin count
        //playerAudio.PlayOneShot(coinSound, 0.1f);
        //coins += count;
        //coinText.text = coins.ToString();
    }

    private void AddDeath()
    {
        //death count
        //deaths += 1;
        //deathText.text = deaths.ToString();
    }

    private void SpawnCorpse()
    {
        // spawn corpse
        GameObject deathPlayer = (GameObject)Instantiate(deadPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
        deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
    }

    private void Respawn()
    {
        // mvoe back to original position and set the player to active
        transform.position = startPos;
        playerGameObject.SetActive(true);

        // set recently died to false so the player is capable of dying
        //recentlyDied = false;
    }

    private void UpdateCounters()
    {
        // pause and save timer
        timerActive = false;
        //scoreCounter.SetFader(fadeTimer);
        //scoreCounter.SetTimer(roundTimer);
        // death and coin counters
        //scoreCounter.SetDeaths(deaths);
        //scoreCounter.SetCoins(coins);
    }
}
