using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject stoneAgeWarriorPrefab;
    public GameObject stoneThrowerPrefab;
    public GameObject stoneGiantPrefab;

    private void Start()
    {
        // Initialisation des âges pour l'exemple
        Age age1 = new Age();
        age1.unitPrefabs = new GameObject[] { stoneAgeWarriorPrefab, stoneThrowerPrefab, stoneGiantPrefab };
        age1.baseHealthIncrease = 100;
        age1.experienceToNextAge = 100;

        gameManager.ages = new Age[] { age1 };
    }
}
