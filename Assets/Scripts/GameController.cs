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
    public int coinCount = 0;
    public Text coinText;
    public int nextScene;



    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;

    }

    private void Update()
    {
        coinText.text = coinCount.ToString();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Enemy"))
        {
            Die();
        }

        else if (collision.CompareTag("SceneSwitch"))
        {
            //SceneManager.LoadScene(nextScene);
            Invoke(nameof(SceneSwitch), 2.0f);
        }

    }

    void SceneSwitch()
    {
        SceneManager.LoadScene(nextScene);
    }


    void Die() 
    {
        playerGameObject.SetActive(false);
        GameObject deathPlayer = (GameObject)Instantiate(deadPrefab, playerGameObject.transform.position, playerGameObject.transform.rotation);
        deathPlayer.transform.localScale = new Vector3(playerGameObject.transform.localScale.x, playerGameObject.transform.localScale.y, playerGameObject.transform.localScale.z);
        Invoke(nameof(Respawn), 5.0f);
    }

    void Respawn()
    {
        transform.position = startPos;
        playerGameObject.SetActive(true);

    }
}
