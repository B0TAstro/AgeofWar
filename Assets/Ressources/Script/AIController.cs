using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject[] unitPrefabs; // Array of unit prefabs for different ages
    public Transform spawnPoint;
    public float spawnInterval = 5f; // Time between spawns
    public int playerGold = 1000; // Initial gold for AI
    private float spawnTimer;
    private Age currentAge;

    private void Start()
    {
        spawnTimer = spawnInterval;
        currentAge = GameManager.instance.ages[0]; // Initial age
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            SpawnUnit();
            spawnTimer = spawnInterval;
        }
    }

    private void SpawnUnit()
    {
        int unitIndex = Random.Range(0, currentAge.unitPrefabs.Length);
        GameObject unitPrefab = currentAge.unitPrefabs[unitIndex];
        int unitCost = unitPrefab.GetComponent<Unit>().cost;

        if (playerGold >= unitCost && Random.value > 0.3f) // 30% chance de ne pas spawn une unité
        {
            playerGold -= unitCost;
            GameObject unit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
            unit.GetComponent<Unit>().isPlayerOne = false;
        }
    }

    public void AddGold(int amount)
    {
        playerGold += amount;
    }

    public void Evolve(Age newAge)
    {
        currentAge = newAge;
    }
}