using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBase : MonoBehaviour
{
    public int health = 500;
    public bool isPlayerOne;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((isPlayerOne && collision.CompareTag("Enemy")) || (!isPlayerOne && collision.CompareTag("PlayerOneUnit")))
        {
            TakeDamage(collision.GetComponent<Unit>().damage);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(isPlayerOne ? "Player One Loses!" : "AI Loses!");
    }
}