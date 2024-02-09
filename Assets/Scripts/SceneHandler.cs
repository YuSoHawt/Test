using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    static SceneHandler instance = null;

    int startingSceneIndex;

    void Start()
    {
        //if there is an instance
        if (!instance)
        {
            //set to current instance
            instance = this;
            //on scene load do stuff
            SceneManager.sceneLoaded += OnSceneLoaded;
            //Debug.Log("Enabled");
            startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    //parameters defined already
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //starting scene not the same as the current scene index then destroy
        if (startingSceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            //set back to null
            instance = null;
            SceneManager.sceneLoaded -= OnSceneLoaded;
            //Debug.Log("Disabled");
            Destroy(gameObject);
        }
    }
}
