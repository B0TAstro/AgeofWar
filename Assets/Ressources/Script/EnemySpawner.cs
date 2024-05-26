using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public UnitData unitData;
    public Transform spawnPoint;
    public float spawnInterval = 5.0f;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnUnit();
            timer = spawnInterval;
        }
    }

    void SpawnUnit()
    {
        GameObject newUnit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
        Unit unitScript = newUnit.GetComponent<Unit>();
        unitScript.unitData = unitData;
        Debug.Log($"Enemy spawned {unitData.unitName}");
    }
}
