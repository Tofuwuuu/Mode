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

    public float damageDelay = 1.0f; // delay between damage Input
    private bool canDamage = true;

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
        
        Debug.Log("Enemy health before damage: " + (currentHealth + damageAmount));
        Debug.Log("Dealing damage to the enemy: " + damageAmount);
        Debug.Log("Enemy health after damage " + currentHealth);
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

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canDamage )
        {
            StartCoroutine(ApplyDamageWithDelay(other.GetComponent<PlayerHealth>()));
        }
    }

    IEnumerator ApplyDamageWithDelay(PlayerHealth playerHealth)
    {
        if (playerHealth == null)
        {
            // Handle the case where playerHealth is null
            yield break;
        }

        canDamage = false;

        Debug.Log("Player health before damage: " + playerHealth.GetCurrentHealth());

        //Dealing damage to the Player
        playerHealth.TakeDamage(damage);

        Debug.Log("Player health after damage: " + playerHealth.GetCurrentHealth());

        // Wait for the specified delay before allowing damage again
        float elapsedTime = 0f;
        while (elapsedTime < damageDelay)
        {
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        canDamage = true; // Allow damage again after the delay

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLayer Excited the Trigger Zone");
        }
    }
}
