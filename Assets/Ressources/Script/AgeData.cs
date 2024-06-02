using UnityEngine;

[CreateAssetMenu(fileName = "AgeData", menuName = "ScriptableObjects/AgeData", order = 1)]
public class AgeData : ScriptableObject
{
    public GameObject meleeUnitPrefab;
    public GameObject rangedUnitPrefab;
    public GameObject tankUnitPrefab;
}
