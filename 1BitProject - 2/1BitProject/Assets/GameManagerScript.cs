using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManagerScript : MonoBehaviour
{
    // Keep track of points
    private int points;
    public int num_enemies_killed;

    // Higher difficulty at later stages
    public int stage;
    const int ENEMIES_PER_STAGE = 3;

    // Chandelure spawn variables
    const int POINTS_TO_SPAWN_CHANDELURE = 180;
    bool chandelureSpawned = false;

    public Spawner spawner;

    public UnityAction<bool> enemyKilled;

    public static event Action OnChandelureKilled;


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

    void OnEnemyKilled(bool isChandelure=false)
    {
        Debug.Log("Enemy kill confirmed");
        points += isChandelure ? 240 : 1;
        num_enemies_killed += 1;

        if (isChandelure)
        {
            OnChandelureKilled?.Invoke();
        }

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

        AudioSingleton.Instance.PlayEnemyKilled();
    
    }


}
