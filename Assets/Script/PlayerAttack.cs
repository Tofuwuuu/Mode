using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackCooldown = 0.5f;
    private float nextAttackTime = 0f;
    public int damageAmount = 10; // Damage output


    private void OnDrawGizmos()
    {
        // Draw gizmoss
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2.0f);
    }
    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextAttackTime)
        {
            Debug.Log("Attack!");
            DamageEnemies();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void DamageEnemies()
    {   
        int enemyLayerMask = LayerMask.GetMask("Enemy");
        Debug.Log("Enemy LayerMask value: " + enemyLayerMask); 
        // Example: Find all GameObjects with the "Enemy" Tag within the certain range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 2.0f, LayerMask.GetMask("Enemy"));
        
        Debug.Log("Number of Colliders found" + hitEnemies.Length);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            // Check if the collided object has an Enemy Script
            Debug.Log("Enemy Detected");
            Enemy enemyScript = enemyCollider.GetComponent<Enemy>();
            if (enemyScript != null)
            {    
                Debug.Log("dealing damage to the enemy");
                // Deal damage
                enemyScript.TakeDamage(damageAmount);
            }
        }
    }
}
