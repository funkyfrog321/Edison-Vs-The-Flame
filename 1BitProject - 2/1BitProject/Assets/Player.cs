using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed; // Our Player's movement speed
    private Rigidbody2D rb2d; //Our Player's Body

    public Weapon weapon;
    public Camera sceneCamera;
    private Vector2 mousePosition;

    public int health;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb2d.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
        // Try out this delta time method??
        //rb2d.transform.position += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);


        
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);

        //Rotate player to follow mouse, used to determine which direction bullets fire
        Vector2 aimDirection = mousePosition - rb2d.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg-90f;
        rb2d.rotation = aimAngle;


        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
        {
            weapon.Fire();
        }



    }


}
