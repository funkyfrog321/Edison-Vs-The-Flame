using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public UnityEvent<bool> killed = new UnityEvent<bool>();


    public Transform opponent; //literally just changed it
    public float moveSpeed;
    public int health;
    public bool isChandelure; 

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (opponent != null)
        {
            // Calculate the direction from this object to the target object
            Vector3 directionToTarget = opponent.position - transform.position;

            // Calculate the distance between this object and the target object
            float distanceToTarget = directionToTarget.magnitude;

            // Normalize the direction vector to get the movement direction
            Vector3 moveDirection = directionToTarget.normalized;

            // Calculate the distance to move this frame based on speed
            float moveDistance = moveSpeed * Time.deltaTime;

            // Limit the move distance to not overshoot the target
            moveDistance = Mathf.Min(moveDistance, distanceToTarget);

            // Move this object towards the target
            transform.position += moveDirection * moveDistance;
        }

        

    }

    public void TakeDamage(int damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        if (isChandelure)
        {
            ChandelureStages();
        }


    }

    private void OnDestroy()
    {
        killed.Invoke(isChandelure);
    }

    private void ChandelureStages()
    {
        if (health <= 0)
        {
            return;
        }

        if (health%40 == 0)
        {
            GetComponent<ChandelureStages>().progressStage();
        }
           

    }

}
