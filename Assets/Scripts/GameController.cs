using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//test
using TMPro;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    public GameObject playerGameObject;
    public GameObject deadPrefab;
    
    public int nextScene;

    // player audio and sound
    private AudioSource playerAudio;
    public AudioClip coinSound;

    // coin count
    private int coinCount = 0;
    public Text coinText;


    // death count
    [SerializeField]
    int deaths;
    DeathCounter deathCounter;
    public Text deathText;



    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        startPos = transform.position;

        //test
        deathCounter = FindObjectOfType<DeathCounter>();
        deaths = deathCounter.CurrentDeaths;
        deathText.text = deaths + "";
    }

    private void Update()
    {
        //coin count
        coinText.text = coinCount.ToString();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            AddDeath();
            Die();
        }

        else if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            AddCoin();
        }

        else if (collision.CompareTag("SceneSwitch"))
        {
            StartCoroutine(SceneSwitch());

            //death count
            deathCounter.SetDeaths(deaths);
        }

    }

    private IEnumerator SceneSwitch()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextScene);
    }


    private void Die() 
    {
        playerGameObject.SetActive(false);
        SpawnCorpse();
        Invoke(nameof(Respawn), 5.0f);
    }

    private void AddCoin()
    {
        playerAudio.PlayOneShot(coinSound, 0.1f);
        // add to coin count
        coinCount += 1;
    }

    private void AddDeath()
    {
        //death count
        deaths += 1;
        deathText.text = deaths + "";
    }

    private void SpawnCorpse()
    {
        GameObject deathPlayer = (GameObject)Instantiate(deadPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
        deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
    }

    private void Respawn()
    {
        transform.position = startPos;
        playerGameObject.SetActive(true);
    }
}
