using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    //bullet enemy class
    public GameObject enemy;
   
    public GameObject player;

    //Spawn Rate for Enemies
    public float spawnRate = 5;

    private float timeout;

    public GameObject plug;
    public GameObject spawnPointsHolder;
    public List<Transform> spawnPoints = new List<Transform>();

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
            spawnEnemy(); 
            timeout = spawnRate; }
    }

    void spawnEnemy()
    {
        int spawn = Random.Range(0, spawnPoints.Count);
        Transform spawnTransform = spawnPoints[spawn];

        Instantiate(enemy, spawnTransform);
        enemy.GetComponent<Enemy>().opponent = player.transform;
        enemy.GetComponent<EnemyDamage>().playerHealth = player.GetComponent<PlayerHealth>();
    }
}
