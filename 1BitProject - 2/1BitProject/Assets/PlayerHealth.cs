using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public static event Action OnPlayerDeath;
    public int health;
    public int maxHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth; // start game with full health
    }

    public void takeDamage(int amount) //Can change how much damage each enemy inflicts upon the player
    {
        health -= amount;
        if (health <= 0)
        {
            health = 0;
            Destroy(gameObject);

            //Display our game over screen
            OnPlayerDeath?.Invoke();

            AudioSingleton.Instance.PlayGameOver();
            AudioSingleton.Instance.StopGameMusic();
        }
        else
        {
            AudioSingleton.Instance.PlayOof();
        }
    }

}
