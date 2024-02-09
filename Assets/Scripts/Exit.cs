using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Delay
        StartCoroutine(LoadNextLvl());
    }

    IEnumerator LoadNextLvl()
    {
        yield return new WaitForSecondsRealtime(loadDelay);
        int currSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currSceneIndex + 1);
    }
}
