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

        if ((isPlayerOne && GameManager.instance.player1Gold >= unitCost) ||
            (!isPlayerOne && GameManager.instance.player2Gold >= unitCost))
        {
            if (isPlayerOne)
                GameManager.instance.player1Gold -= unitCost;
            else
                GameManager.instance.player2Gold -= unitCost;

            GameObject unit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
            unit.GetComponent<Unit>().isPlayerOne = isPlayerOne;
        }
    }
}
