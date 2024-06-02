using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject unitPrefab;
    public UnitData unitData;
    public Transform spawnPoint;
    public Button spawnButton;

    void Start()
    {
        spawnButton.onClick.AddListener(SpawnUnit);
    }

    void SpawnUnit()
    {
        if (gameManager.SpendGold(unitData.cost))
        {
            GameObject newUnit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
            Unit unitScript = newUnit.GetComponent<Unit>();
            unitScript.unitData = unitData;
            Debug.Log($"Spawned {unitData.unitName}");
        }
        else
        {
            Debug.Log("Not enough gold to spawn unit.");
        }
    }
}
