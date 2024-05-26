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
    public int cost; // Coût de l'unité en or
    public int goldReward; // Or gagné en tuant une unité ennemie
    public int xpReward; // XP gagné en tuant une unité ennemie
}
