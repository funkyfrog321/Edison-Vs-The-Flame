using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{

    //bullet enemy class
    public GameObject enemy;
    public GameObject chandelureSpawn;
    public GameObject chandelure;

    public GameObject player;

    //Spawn Rate for Enemies
    public float spawnRate = 5;

    private float timeout;

    public GameObject plug;
    public GameObject spawnPointsHolder;
    public List<Transform> spawnPoints = new List<Transform>();

    public UnityAction<bool> onEnemyKilled;
    //public UnityEvent<int> enemyKilled;
    


    // Start is called before the first frame update
    void Start()
    {
        timeout = spawnRate;

        // Build the list of spawn points
        for (int i = 0; i < spawnPointsHolder.transform.childCount; i++)
        {
            spawnPoints.Add(spawnPointsHolder.transform.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Process the timer that tells when to spawn new enemies
        // Thanks to GibTreaty https://forum.unity.com/threads/why-no-timer-class.221139/#post-1474605
        if (timeout > 0) {
            timeout -= Time.deltaTime;
            if (timeout < 0) 
                timeout = 0;
        }
        if (timeout == 0) { 
            // Timeout: spawn an enemy and reset the timer
            timeout = spawnRate;
            SpawnEnemy();  
        }
    }

    public void SetSpawnRate(int stage)
    {
        //spawnRate = 1 / (Mathf.Pow(1.1f, stage - 16.9f));
        spawnRate = 1 / (0.05f * (stage + 4));
        Debug.Log("Entered stage " + stage + " ;  spawn rate: " + spawnRate);
    }

    void SpawnEnemy()
    {
        int spawn = Random.Range(0, spawnPoints.Count);
        Transform spawnTransform = spawnPoints[spawn];

        GameObject new_enemy = Instantiate(enemy, spawnTransform);
        new_enemy.GetComponent<Enemy>().opponent = player.transform;
        new_enemy.GetComponent<EnemyDamage>().playerHealth = player.GetComponent<PlayerHealth>();

        new_enemy.GetComponent<Enemy>().killed.AddListener((bool isChandy) => onEnemyKilled(isChandy));
    }

    public void SpawnChandelure()
    {
        Debug.Log("Chandelure spawned");
        Transform spawnTransform = chandelureSpawn.GetComponent<Transform>();

        GameObject newChandelure = Instantiate(chandelure, spawnTransform);
        newChandelure.GetComponent<Enemy>().opponent = player.transform;
        newChandelure.GetComponent<EnemyDamage>().playerHealth = player.GetComponent<PlayerHealth>();

        newChandelure.GetComponent<Enemy>().killed.AddListener((bool isChandy) => onEnemyKilled(isChandy));
    }
}
