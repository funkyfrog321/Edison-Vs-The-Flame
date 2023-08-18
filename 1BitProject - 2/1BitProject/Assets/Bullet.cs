using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public Rigidbody2D rb;
    // Bullet will be destroyed after some time
    public float max_lifetime = 0;
    public float lifetime = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // max_lifetime of 0 means the bullet never times out
        if (max_lifetime > 0)
        {
            lifetime += Time.deltaTime;
            if (lifetime > max_lifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemyScript = collision.gameObject.GetComponent<Enemy>();
            enemyScript.TakeDamage();
            Destroy(gameObject);
        }
    }
}
