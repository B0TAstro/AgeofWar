using UnityEngine;

[CreateAssetMenu(fileName = "AgeData", menuName = "ScriptableObjects/AgeData", order = 2)]
public class AgeData : ScriptableObject
{
    public string ageName;
    public int baseHealthIncrease;
    public UnitData[] newUnits;
}
