using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int coinPts = 100;
    [SerializeField] AudioClip coinSound;

    //state
    bool addedToScore = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check so that a coin can only add to the total value once
        if (collision.tag == "Player" && !addedToScore)
        {
            addedToScore = true;
            FindObjectOfType<GameManager>().AddScore(coinPts);
            AudioSource.PlayClipAtPoint(coinSound, Camera.main.transform.position);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }

    }
}
