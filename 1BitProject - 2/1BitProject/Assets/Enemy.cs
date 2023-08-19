using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform opponent; //literally just changed it 
    public float moveSpeed;
    

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

    private void OnDestroy()
    {
        Debug.Log("Enemy destroyed");
        // Remove this glow from the static list of lights
        //Transform glow = transform.GetChild(0);
        //if (glow.name != "Glow")
        //{
        //    Debug.LogError("Child is not a glow object");
        //    return;
        //}
        //glow.GetComponent<InitializeGlowShader>().RemoveLight();
    }
}
