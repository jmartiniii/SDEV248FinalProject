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
    private AudioSource playerAudio;
    public AudioClip coinSound;

    // reference score counter
    ScoreCounter scoreCounter;

    // coin count
    [SerializeField] int coins;
    public Text coinText;

    // death count
    [SerializeField] int deaths;
    public Text deathText;
    
    private bool recentlyDied;



    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        scoreCounter = FindObjectOfType<ScoreCounter>();

        startPos = transform.position;

        deaths = scoreCounter.currentDeaths;
        deathText.text = deaths.ToString();
        coins = scoreCounter.currentCoins;
        coinText.text = coins.ToString();

        recentlyDied = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            // set recently died to prevent multiple deaths
            // die and add death
            if (!recentlyDied)
            {
                recentlyDied = true;
                Die();
                AddDeath();                
            }
        }

        else if (collision.CompareTag("Coin"))
        {
            // add coins
            Destroy(collision.gameObject);
            AddCoin();
        }

        else if (collision.CompareTag("SceneSwitch"))
        {
            // switch scene
            UpdateCounters();
            StartCoroutine(SceneSwitch());
        }

    }

    private IEnumerator SceneSwitch()
    {
        // wait 2 seconds then load the next scene
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    private void Die() 
    {
        //deactivate player, spawn corpse, respawn
        playerGameObject.SetActive(false);
        SpawnCorpse();
        Invoke(nameof(Respawn), 5.0f);
    }

    private void AddCoin()
    {
        //add to coin count
        playerAudio.PlayOneShot(coinSound, 0.1f);
        coins += 1;
        coinText.text = coins.ToString();
    }

    private void AddDeath()
    {
        //death count
        deaths += 1;
        deathText.text = deaths.ToString();
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
        recentlyDied = false;
    }

    private void UpdateCounters()
    {
        // death and coin counters
        scoreCounter.SetDeaths(deaths);
        scoreCounter.SetCoins(coins);
    }
}
