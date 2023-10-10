using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public HealthBar healthBar;
    float speed = 0.5f;
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

        if(health <= 0)
        {
            // Load the end screen scene
            SceneManager.LoadScene("End");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Trigger enter");
        if (collision.gameObject.tag == "Bullet")
        {
            health -= 10;
        }
        if (collision.gameObject.tag == "Health Potion")
        {
            health += 10;
            Destroy(collision.gameObject);
        }
    }


    
}
