using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
     void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            PlayerDefeat();
        }

        // Can add health UI update here
    }

    void PlayerDefeat()
    {
        Debug.Log("Player defeated! ");

        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
    void Update()
    {
        
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
