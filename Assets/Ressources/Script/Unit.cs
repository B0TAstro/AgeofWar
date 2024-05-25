using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int health;
    public int damage;
    public int cost;
    public float moveSpeed;
    public bool isPlayerOne;

    private Transform target;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (isPlayerOne)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        if (target != null)
        {
            // Attack logic
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
        Destroy(gameObject);
        // Add logic to reward gold and experience to the attacker
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((isPlayerOne && collision.CompareTag("Enemy")) || (!isPlayerOne && collision.CompareTag("PlayerOneUnit")))
        {
            target = collision.transform;
        }
    }
}