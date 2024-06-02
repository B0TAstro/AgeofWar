using UnityEngine;

public class Base : MonoBehaviour
{
    public int maxHealth = 500;
    private int currentHealth;
    public GameManager gameManager;
    public int teamId;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Base {teamId} takes {damage} damage. Remaining health: {currentHealth}.");
        if (currentHealth <= 0)
        {
            Debug.Log($"Base {teamId} has been destroyed.");
            gameManager.EndGame(teamId);
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
