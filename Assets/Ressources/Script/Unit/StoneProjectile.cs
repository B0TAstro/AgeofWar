using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneProjectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void SetDamage(int damageAmount)
    {
        damage = damageAmount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
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
