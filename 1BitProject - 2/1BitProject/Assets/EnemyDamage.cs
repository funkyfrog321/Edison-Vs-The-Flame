using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    
    public PlayerHealth playerHealth;
    public int damage = 2;


    
    //https://www.youtube.com/watch?v=_1Oou4459Us - Night Run Studios helped me with everything :)
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Owie");
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.takeDamage(damage);
            
        }
    }
}
