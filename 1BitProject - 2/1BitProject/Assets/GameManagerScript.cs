using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerScript : MonoBehaviour
{
    // TODO : exponentially increase spawn rate

    // Keep track of points
    private int points;

    // Chandelure spawn variables
    const int POINTS_TO_SPAWN_CHANDELURE = 5;
    bool chandelureSpawned = false;

    public Spawner spawner;

    public UnityAction<int> enemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;

        spawner.onEnemyKilled += OnEnemyKilled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnemyKilled(int pointValue=1)
    {
        Debug.Log("Enemy kill confirmed");
        points += pointValue;
        
        if (!chandelureSpawned && points >= POINTS_TO_SPAWN_CHANDELURE)
        {
            spawner.SpawnChandelure();
            chandelureSpawned = true;
        }
    }
}
