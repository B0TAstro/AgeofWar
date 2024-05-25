using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneAgeThrower : MonoBehaviour
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
        base.Update();
        // Ajoutez la logique pour lancer des pierres à distance
    }

    private void ThrowStone()
    {
        GameObject stone = Instantiate(stonePrefab, throwPoint.position, throwPoint.rotation);
        stone.GetComponent<Projectile>().SetDamage(damage);
    }
}