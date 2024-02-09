using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoresText;
    [SerializeField] TextMeshProUGUI highScoreText;

    void Awake()
    {
        int numOfGameSessions = FindObjectsOfType<GameManager>().Length;

        if (numOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "End Menu")
        {
            livesText.text = " ";
            scoresText.text = " ";
            //add High score to the middle for finishing
            highScoreText.text = "Your Total Score: " + score.ToString();
        }
        else
        {
            livesText.text = playerLives.ToString();
            scoresText.text = score.ToString();
            highScoreText.text = "";
        }
    }

    //Add score to each coin
    public void AddScore(int pts)
    {
        score += pts;
        //use for db later
        scoresText.text = score.ToString();
    }

    //Process each time a player dies
    public void ProcessPLayerDeath()
    {
        if (playerLives > 1)
        {
            SubtractLife();
        }
        else
        {
            //added this
            //FindObjectOfType<SceneHandler>().WipeScenePersistence();
            DeathResetGame();
        }
    }


    private void SubtractLife()
    {
        StartCoroutine(WaitTimeCurrent());
    }

    //pause execution resumes at next frame
    IEnumerator WaitTimeCurrent()
    {
        yield return new WaitForSeconds(2);
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    private void DeathResetGame()
    {
        /*
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        */
        StartCoroutine(WaitTimeReset());
    }

    IEnumerator WaitTimeReset()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Death Screen");
        //SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
