using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneAgeThrower : Unit
{
    public GameObject stonePrefab;
    public Transform throwPoint;

    private void Start()
    {
        health = 70;
        damage = 15;
        cost = 75;
        moveSpeed = 1.5f;
        isPlayerOne = true; // Ou false pour l'IA
    }

    private void Update()
    {
        Move();
        // Ajoutez la logique pour lancer des pierres à distance
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
    }

    private void ThrowStone()
    {
        GameObject stone = Instantiate(stonePrefab, throwPoint.position, throwPoint.rotation);
        stone.GetComponent<StoneProjectile>().SetDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((isPlayerOne && collision.CompareTag("Enemy")) || (!isPlayerOne && collision.CompareTag("PlayerOneUnit")))
        {
            ThrowStone();
        }
    }
}