using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObjects/UnitData", order = 1)]
public class UnitData : ScriptableObject
{
    public string unitName;
    public int health;
    public int attackPower;
    public float speed;
    public float attackInterval;
    public float attackRange;
    public int teamId;
    public int cost;
    public int goldReward;
    public int xpReward;
}
