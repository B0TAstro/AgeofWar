using UnityEngine;

public class Base : MonoBehaviour
{
    public int teamId;
    private int health;
    private int maxHealth; // Added maxHealth variable

    void Start()
    {
        health = 100;
        maxHealth = 100; // Initialize maxHealth
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Logic for game over or base destruction
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        health += amount; // Also increase current health
    }
}