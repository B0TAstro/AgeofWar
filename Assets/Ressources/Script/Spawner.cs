using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] unitPrefabs;
    public Transform spawnPoint;
    public bool isPlayerOne;

    public void SpawnUnit(int unitIndex)
    {
        GameObject unitPrefab = unitPrefabs[unitIndex];
        int unitCost = unitPrefab.GetComponent<Unit>().cost;

        if ((isPlayerOne && GameManager.instance.playerGold >= unitCost) ||
            (!isPlayerOne && GameManager.instance.aiGold >= unitCost))
        {
            if (isPlayerOne)
                GameManager.instance.playerGold -= unitCost;
            else
                GameManager.instance.aiGold -= unitCost;

            GameObject unit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
            unit.GetComponent<Unit>().isPlayerOne = isPlayerOne;
        }
    }
}