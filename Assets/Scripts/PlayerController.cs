using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);
        rb.velocity = movement * moveSpeed;

        // Shooting
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Get mouse position in world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Calculate direction from player to mouse
        Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;
        
        // Instantiate the bullet
        GameObject bulletInstance = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bulletInstance.GetComponent<Rigidbody2D>();
        
        // Set the bullet's velocity in the direction of the mouse
        bulletRb.velocity = shootDirection * bulletSpeed;

        Destroy(bulletRb.gameObject, 2);
    }
}
