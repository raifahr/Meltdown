using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    
    public int healthBar;  

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        // update ui
        if (healthBar != null)
        {
            healthBar = currentHealth;
        }
        
        // check if dead
        if (currentHealth <= 0)
        {
            Debug.Log("Died");
        }
    }

}