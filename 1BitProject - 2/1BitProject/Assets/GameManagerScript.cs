using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManagerScript : MonoBehaviour
{
    // Keep track of points
    private int points;
    private int num_enemies_killed;

    // Higher difficulty at later stages
    private int stage;
    const int ENEMIES_PER_STAGE = 3;

    // Chandelure spawn variables
    const int POINTS_TO_SPAWN_CHANDELURE = 5;
    bool chandelureSpawned = false;

    public Spawner spawner;

    public UnityAction<int> enemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        num_enemies_killed = 0;
        stage = 0;

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
        num_enemies_killed += 1;

        if (num_enemies_killed % ENEMIES_PER_STAGE == 0)
        {
            stage++;
            spawner.SetSpawnRate(stage);
        }    

        if (!chandelureSpawned && points >= POINTS_TO_SPAWN_CHANDELURE)
        {
            spawner.SpawnChandelure();
            chandelureSpawned = true;
        }
    }
}
