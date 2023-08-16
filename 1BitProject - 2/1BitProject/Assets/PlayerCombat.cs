using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{


    public GameObject boltPrefab;       // Reference to the bolt prefab to be shot
    public Transform firePoint;         // The point where the bolt will be spawned
    public float boltSpeed = 100f;       // Speed of the bolt
    public float shootCooldown = 0.5f;  // Cooldown time between shots

    private float shootTimer = 0f;      // Timer to track cooldown

    private void Update()
    {
        // Update the cooldown timer
        shootTimer -= Time.deltaTime;

        //Get The Location of the Mouse
        Vector2 mousePosition = Input.mousePosition;

        // Check for user input to shoot
        if (Input.GetButtonDown("Fire1") && shootTimer <= 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }
    }

    private void Shoot()
    {
        // Instantiate a bolt at the fire point position and rotation
        GameObject newBolt = Instantiate(boltPrefab, firePoint.position, firePoint.rotation);


        // Access the Rigidbody of the bolt and apply force to propel it forward
        Rigidbody boltRigidbody = newBolt.GetComponent<Rigidbody>();
        if (boltRigidbody != null)
        {
            boltRigidbody.velocity = firePoint.forward * boltSpeed;
        }
    }
}
