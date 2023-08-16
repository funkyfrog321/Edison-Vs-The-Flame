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
            Debug.Log(i);
            Debug.Log(spawnPointsHolder.transform.GetChild(i).name);
            spawnPoints.Add(spawnPointsHolder.transform.GetChild(i));
        }
        Debug.Log("spawnPoints Count: " + spawnPoints.Count);
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
        Debug.Log(spawn + ", with a max of " +  (spawnPoints.Count - 1));
        Transform spawnTransform = spawnPoints[spawn];
        Debug.Log(spawn + ";  " + ";  x: " + spawnTransform.position.x + ";  z: " + spawnTransform.position.z );

        Instantiate(enemy, spawnTransform);
        enemy.GetComponent<Enemy>().plug = plug.transform;

    }
}
