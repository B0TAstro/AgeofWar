using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Age[] ages;
    public GameObject[] playerUnits;
    public GameObject[] aiUnits;
    public Transform playerSpawnPoint;
    public Transform aiSpawnPoint;

    public int playerGold = 100;
    public int aiGold = 100;
    public int playerExperience = 0;
    public int aiExperience = 0;
    public int experienceToNextAge = 100;

    private int playerAge = 0;
    private int aiAge = 0;

    private float aiSpawnTimer = 0;
    private float aiSpawnInterval = 5f; // Adjust this value for AI spawn rate

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (playerExperience >= experienceToNextAge)
        {
            playerExperience -= experienceToNextAge;
            playerAge++;
        }

        if (aiExperience >= experienceToNextAge)
        {
            aiExperience -= experienceToNextAge;
            aiAge++;
        }

        aiSpawnTimer += Time.deltaTime;
        if (aiSpawnTimer >= aiSpawnInterval)
        {
            SpawnAIUnit();
            aiSpawnTimer = 0;
        }
    }

    public void SpawnPlayerUnit(int unitIndex)
    {
        if (unitIndex < playerUnits.Length && playerGold >= playerUnits[unitIndex].GetComponent<Unit>().cost)
        {
            Instantiate(playerUnits[unitIndex], playerSpawnPoint.position, Quaternion.identity);
            playerGold -= playerUnits[unitIndex].GetComponent<Unit>().cost;
        }
    }

    private void SpawnAIUnit()
    {
        int unitIndex = Random.Range(0, aiUnits.Length);
        if (aiGold >= aiUnits[unitIndex].GetComponent<Unit>().cost)
        {
            Instantiate(aiUnits[unitIndex], aiSpawnPoint.position, Quaternion.identity);
            aiGold -= aiUnits[unitIndex].GetComponent<Unit>().cost;
        }
    }

    public void RewardPlayer(int gold, int experience)
    {
        playerGold += gold;
        playerExperience += experience;
    }

    public void RewardAI(int gold, int experience)
    {
        aiGold += gold;
        aiExperience += experience;
    }
}