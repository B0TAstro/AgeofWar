using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneProjectile : MonoBehaviour
{
    public float speed = 10f;
    private int damage;
    private bool isPlayerOne;

    private void Update()
    {
        transform.Translate(isPlayerOne ? Vector2.right : Vector2.left * speed * Time.deltaTime);
    }

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((isPlayerOne && collision.CompareTag("Enemy")) || (!isPlayerOne && collision.CompareTag("PlayerOneUnit")))
        {
            collision.GetComponent<Unit>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Base"))
        {
            collision.GetComponent<PlayerBase>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
