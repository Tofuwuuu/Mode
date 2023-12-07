using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public int maxHealth = 100;
    public int damage = 10;

    private int currentHealth;

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        currentHealth = maxHealth;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            player = players[0].transform;
        }
        else
        {
            Debug.LogError("No Player Gameobjectfound");
        }

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            direction.Normalize();
            rb.velocity = direction * speed;
        }
        else
        {
            // Handle the case where the palyer is not found
            Debug.LogError("Player not Found!");
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            DefeatEnemy();
        }
    }

    void DefeatEnemy()
    {
        Debug.Log("Enemy defeated!");

        // Can add additional logic for enemy defeat like playing animation.
        Destroy(gameObject);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered");

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                Debug.Log("Player health before damage: " + playerHealth.GetCurrentHealth());
                Debug.Log("Dealing damage to the player");
                playerHealth.TakeDamage(damage);
                 Debug.Log("Player health after damage: " + playerHealth.GetCurrentHealth());
            }

            Debug.Log("Destroing the enemy");
        }
    }
}
