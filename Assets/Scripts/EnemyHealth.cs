using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public HealthBar healthBar;
    void Start()
    {
        health = maxHealth;
        healthBar.SetHealth(health);
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(health);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger enter");
        if (collision.gameObject.tag == "BulletPlayer")
        {
            health -= 10;
        }
    }


    
}
