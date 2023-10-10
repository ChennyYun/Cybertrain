using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float lineOfSight = 2f;
    public float shootingRange = 1f;
    public GameObject bullet;
    public GameObject bulletParent;
    Transform player;
    float shootingCooldown = 1f;
    float nextShootTime = 0f;
    float nextRandomMoveTime = 0f;
    public float moveCoolDown = 2f;

    public int health = 100;
    public int maxHealth = 100;
    public HealthBar healthBar;
    bool running = false;

    Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        healthBar.SetHealth(health);
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            health -= 10;
        } */

        healthBar.SetHealth(health);
        //anim.SetFloat("speed", speed);
        //anim.SetBool("attacking", true);

        if (health <= 0)
        {
            anim.SetBool("dead", true);
            Destroy(this.gameObject, 0.7f);
        }


        float distFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distFromPlayer < lineOfSight && distFromPlayer > shootingRange)
        {
            running = true;
            anim.SetBool("running", true);
            anim.SetBool("attacking", false);
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            running = false;

        }
        else if (distFromPlayer <= shootingRange && nextShootTime < Time.time)
        {
            anim.SetBool("attacking", true);
            anim.SetBool("running", false);
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            nextShootTime = Time.time + shootingCooldown;
        }
        else if (distFromPlayer > lineOfSight) //&& nextRandomMoveTime >= Time.time) 
        {
            anim.SetBool("running", false);
            anim.SetBool("attacking", false);
            /*
            if (nextRandomMoveTime < Time.time)
            {
                anim.SetBool("running", true);
                float xDist = Random.Range(-.5f, .5f);
                float yDist = Random.Range(-.5f, .5f);

                Vector2 movePoint = new Vector2((transform.position.x + xDist), (transform.position.y + yDist));
                Transform move = transform;
                move.position = movePoint;
                print(move.position);

                transform.position = Vector2.MoveTowards(this.transform.position, move.position, speed * Time.deltaTime);
                nextRandomMoveTime = Time.time + moveCoolDown;
            } */
        }
        /*
        else if (distFromPlayer > lineOfSight && nextRandomMoveTime < Time.time)
        {
            anim.SetBool("running", true);
            //generate random move point
            float xDist = Random.Range(-.5f, .5f);
            float yDist = Random.Range(-.5f, .5f);
            
            Vector2 movePoint = new Vector2((transform.position.x + xDist), (transform.position.y + yDist));
            Transform move = transform;
            move.position = movePoint;
            print(move.position);

            transform.position = Vector2.MoveTowards(this.transform.position, move.position, speed * Time.deltaTime);
            nextRandomMoveTime = Time.time + moveCoolDown;
        } */
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
